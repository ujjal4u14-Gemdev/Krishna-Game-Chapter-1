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
            if (puzzleDirector == null) return;
            puzzleDirector.StateChanged += OnStateChanged;
            OnStateChanged(puzzleDirector.State);
        }

        private void OnDisable()
        {
            if (puzzleDirector != null) puzzleDirector.StateChanged -= OnStateChanged;
        }

        private void OnStateChanged(PuzzleState state)
        {
            if (interactionRoot != null) interactionRoot.SetActive(state == PuzzleState.AwaitingInput);
            if (correctFeedback != null) correctFeedback.SetActive(state == PuzzleState.Success);
            if (wrongFeedback != null) wrongFeedback.SetActive(state == PuzzleState.Failure);
        }
    }
}
