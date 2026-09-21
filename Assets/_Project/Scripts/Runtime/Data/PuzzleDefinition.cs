using System;
using System.Collections.Generic;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    [CreateAssetMenu(menuName = "Mythic Puzzle/Puzzle Definition", fileName = "PZ_NewPuzzle")]
    public sealed class PuzzleDefinition : ScriptableObject
    {
        [SerializeField] private string puzzleId;
        [SerializeField] private PuzzleType puzzleType;
        [SerializeField] private List<string> correctEntityIds = new();
        [SerializeField] private List<string> orderedEntityIds = new();
        [SerializeField] private List<EntityPair> validPairs = new();
        [SerializeField] private SwipeDirection correctSwipeDirection;
        [SerializeField, Min(0)] private int requiredCount = 1;
        [SerializeField, Min(0)] private int maxAttempts;
        [SerializeField, Min(0)] private float timeLimitSeconds;
        [SerializeField, Range(0.05f, 1f)] private float requiredProgress = 0.7f;
        [SerializeField] private bool resetAfterFailure = true;
        [SerializeField] private ActionSequenceAsset introSequence;
        [SerializeField] private ActionSequenceAsset successSequence;
        [SerializeField] private ActionSequenceAsset failureSequence;

        public string PuzzleId => puzzleId;
        public PuzzleType PuzzleType => puzzleType;
        public IReadOnlyList<string> CorrectEntityIds => correctEntityIds;
        public IReadOnlyList<string> OrderedEntityIds => orderedEntityIds;
        public IReadOnlyList<EntityPair> ValidPairs => validPairs;
        public SwipeDirection CorrectSwipeDirection => correctSwipeDirection;
        public int RequiredCount => requiredCount;
        public int MaxAttempts => maxAttempts;
        public float TimeLimitSeconds => timeLimitSeconds;
        public float RequiredProgress => requiredProgress;
        public bool ResetAfterFailure => resetAfterFailure;
        public ActionSequenceAsset IntroSequence => introSequence;
        public ActionSequenceAsset SuccessSequence => successSequence;
        public ActionSequenceAsset FailureSequence => failureSequence;
    }

    [Serializable]
    public struct EntityPair
    {
        public string sourceId;
        public string destinationId;
    }
}
