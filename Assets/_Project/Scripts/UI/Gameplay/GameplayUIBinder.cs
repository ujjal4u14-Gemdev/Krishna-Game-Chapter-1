using MythicPuzzle.Runtime;
using UnityEngine;

namespace MythicPuzzle.UI
{
    public sealed class GameplayUIBinder : MonoBehaviour
    {
        [SerializeField] private PuzzleDirector puzzleDirector;
        [SerializeField] private GameObject interactionRoot;
        [SerializeField] private GameObject correctFeedback;
        [SerializeField] private GameObject wrongFeedback;

        private void OnEnable()
        {
            puzzleDirector.StateChanged += OnStateChanged;
            OnStateChanged(puzzleDirector.State);
        }

        private void OnDisable() => puzzleDirector.StateChanged -= OnStateChanged;

        private void OnStateChanged(PuzzleState state)
        {
            interactionRoot.SetActive(state == PuzzleState.AwaitingInput);
            correctFeedback.SetActive(state == PuzzleState.Success);
            wrongFeedback.SetActive(state == PuzzleState.Failure);
        }
    }
}

