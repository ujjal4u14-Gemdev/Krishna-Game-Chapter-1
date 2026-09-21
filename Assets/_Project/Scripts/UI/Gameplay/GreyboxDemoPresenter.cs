using MythicPuzzle.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace MythicPuzzle.UI
{
    public sealed class GreyboxDemoPresenter : MonoBehaviour
    {
        [SerializeField] private PuzzleDirector puzzleDirector;
        [SerializeField] private Text instructionText;
        [SerializeField] private Text statusText;

        private static readonly string[] Instructions =
        {
            "PUZZLE 1/3 — ERASE\nDrag across the blue tiles to reveal the butter.",
            "PUZZLE 2/3 — CHOICE\nClick the GREEN safe route.",
            "PUZZLE 3/3 — AIM & SHOOT\nDrag backward from the launcher, then release. Hit the GOLD pot."
        };

        private void OnEnable()
        {
            if (puzzleDirector == null) return;
            puzzleDirector.PuzzleChanged += OnPuzzleChanged;
            puzzleDirector.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            if (puzzleDirector == null) return;
            puzzleDirector.PuzzleChanged -= OnPuzzleChanged;
            puzzleDirector.StateChanged -= OnStateChanged;
        }

        private void OnPuzzleChanged(int index)
        {
            if (instructionText != null && index >= 0 && index < Instructions.Length)
                instructionText.text = Instructions[index];
            if (statusText != null) statusText.text = string.Empty;
        }

        private void OnStateChanged(PuzzleState state)
        {
            if (statusText == null) return;
            switch (state)
            {
                case PuzzleState.Success:
                    statusText.text = "CORRECT!";
                    statusText.color = new Color(0.25f, 0.85f, 0.35f);
                    break;
                case PuzzleState.Failure:
                    statusText.text = "TRY AGAIN";
                    statusText.color = new Color(1f, 0.3f, 0.25f);
                    break;
                case PuzzleState.AwaitingInput:
                    statusText.text = string.Empty;
                    break;
                case PuzzleState.Complete:
                    instructionText.text = "GREYBOX DEMO COMPLETE";
                    statusText.text = "ALL THREE PUZZLE SYSTEMS PASSED";
                    statusText.color = new Color(1f, 0.8f, 0.2f);
                    break;
            }
        }
    }
}
