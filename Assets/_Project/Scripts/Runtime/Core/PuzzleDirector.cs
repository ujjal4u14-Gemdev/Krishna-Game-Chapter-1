using System;
using System.Collections;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public sealed class PuzzleDirector : MonoBehaviour
    {
        [SerializeField] private LevelDefinition level;
        [SerializeField] private PuzzleInputRouter inputRouter;
        [SerializeField] private ActionSequenceRunner sequenceRunner;

        private int puzzleIndex;
        private int attempts;
        private PuzzleDefinition currentDefinition;
        private IPuzzleEvaluator currentEvaluator;

        public PuzzleState State { get; private set; }
        public LevelDefinition Level => level;
        public int PuzzleIndex => puzzleIndex;
        public int PuzzleCount => level == null ? 0 : level.Puzzles.Count;
        public event Action<PuzzleState> StateChanged;
        public event Action<int> PuzzleChanged;
        public event Action<LevelDefinition> LevelCompleted;
        public event Action<PuzzleDefinition> PuzzleHardFailed;

        private void OnEnable()
        {
            if (inputRouter != null) inputRouter.SignalRaised += OnSignalRaised;
        }

        private void OnDisable()
        {
            if (inputRouter != null) inputRouter.SignalRaised -= OnSignalRaised;
        }

        private IEnumerator Start()
        {
            if (level == null || inputRouter == null || sequenceRunner == null || level.Puzzles.Count == 0)
            {
                Debug.LogError("PuzzleDirector requires a level, input router, sequence runner and at least one puzzle.");
                yield break;
            }

            yield return BeginPuzzle(0);
        }

        private IEnumerator BeginPuzzle(int index)
        {
            inputRouter.InputEnabled = false;
            puzzleIndex = index;
            attempts = 0;
            currentDefinition = level.Puzzles[puzzleIndex];
            currentEvaluator = PuzzleEvaluatorRegistry.Create(currentDefinition.PuzzleType);
            currentEvaluator.Initialize(currentDefinition);
            PuzzleChanged?.Invoke(puzzleIndex);

            SetState(PuzzleState.Intro);
            yield return sequenceRunner.Run(currentDefinition.IntroSequence);
            SetState(PuzzleState.AwaitingInput);
            inputRouter.InputEnabled = true;
        }

        private void OnSignalRaised(PuzzleInputSignal signal)
        {
            if (State != PuzzleState.AwaitingInput) return;

            var result = currentEvaluator.Evaluate(signal);
            switch (result)
            {
                case PuzzleEvaluation.Continue:
                    break;
                case PuzzleEvaluation.Correct:
                    StartCoroutine(ResolveSuccess());
                    break;
                case PuzzleEvaluation.Incorrect:
                    StartCoroutine(ResolveFailure());
                    break;
            }
        }

        private IEnumerator ResolveSuccess()
        {
            inputRouter.InputEnabled = false;
            SetState(PuzzleState.Success);
            yield return sequenceRunner.Run(currentDefinition.SuccessSequence);

            if (puzzleIndex + 1 < level.Puzzles.Count)
            {
                yield return BeginPuzzle(puzzleIndex + 1);
            }
            else
            {
                yield return sequenceRunner.Run(level.CompletionSequence);
                SetState(PuzzleState.Complete);
                LevelCompleted?.Invoke(level);
            }
        }

        private IEnumerator ResolveFailure()
        {
            inputRouter.InputEnabled = false;
            attempts++;
            SetState(PuzzleState.Failure);
            yield return sequenceRunner.Run(currentDefinition.FailureSequence);

            var hardFail = currentDefinition.MaxAttempts > 0 && attempts >= currentDefinition.MaxAttempts;
            if (hardFail)
            {
                SetState(PuzzleState.Complete);
                PuzzleHardFailed?.Invoke(currentDefinition);
                yield break;
            }

            if (currentDefinition.ResetAfterFailure)
            {
                SetState(PuzzleState.Resetting);
                currentEvaluator.Reset();
            }

            SetState(PuzzleState.AwaitingInput);
            inputRouter.InputEnabled = true;
        }

        private void SetState(PuzzleState state)
        {
            State = state;
            StateChanged?.Invoke(state);
        }
    }
}
