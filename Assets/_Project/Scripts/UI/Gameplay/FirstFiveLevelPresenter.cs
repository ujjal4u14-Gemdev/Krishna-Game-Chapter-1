using MythicPuzzle.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicPuzzle.UI
{
    /// <summary>HUD and navigation for the Levels 1-5 production slice.</summary>
    public sealed class FirstFiveLevelPresenter : MonoBehaviour
    {
        [SerializeField] private PuzzleDirector puzzleDirector;
        [SerializeField] private Text levelText;
        [SerializeField] private Text objectiveText;
        [SerializeField] private Text feedbackText;
        [SerializeField] private GameObject completionPanel;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private string objective;
        [SerializeField] private string nextSceneName;

        private void OnEnable()
        {
            if (puzzleDirector != null)
            {
                puzzleDirector.StateChanged += OnStateChanged;
                puzzleDirector.LevelCompleted += OnLevelCompleted;
                if (levelText != null && puzzleDirector.Level != null)
                    levelText.text = $"LEVEL {puzzleDirector.Level.LevelNumber}";
            }

            if (objectiveText != null) objectiveText.text = objective;
            if (completionPanel != null) completionPanel.SetActive(false);
            if (nextButton != null) nextButton.onClick.AddListener(LoadNext);
            if (retryButton != null) retryButton.onClick.AddListener(Retry);
        }

        private void OnDisable()
        {
            if (puzzleDirector != null)
            {
                puzzleDirector.StateChanged -= OnStateChanged;
                puzzleDirector.LevelCompleted -= OnLevelCompleted;
            }

            if (nextButton != null) nextButton.onClick.RemoveListener(LoadNext);
            if (retryButton != null) retryButton.onClick.RemoveListener(Retry);
        }

        private void OnStateChanged(PuzzleState state)
        {
            if (feedbackText == null) return;
            feedbackText.text = state switch
            {
                PuzzleState.Success => "WELL DONE!",
                PuzzleState.Failure => "TRY AGAIN",
                _ => string.Empty
            };
        }

        private void OnLevelCompleted(LevelDefinition level)
        {
            // Level 1 owns a timed cinematic and advances automatically after its close-up.
            if (level != null && level.LevelNumber == 1) return;
            if (completionPanel != null) completionPanel.SetActive(true);
            if (feedbackText != null) feedbackText.text = "MODAK FOUND!";
            if (nextButton != null)
            {
                nextButton.gameObject.SetActive(!string.IsNullOrWhiteSpace(nextSceneName));
                var label = nextButton.GetComponentInChildren<Text>();
                if (label != null) label.text = string.IsNullOrWhiteSpace(nextSceneName) ? "COMPLETE" : "NEXT LEVEL";
            }
        }

        public void LoadNext()
        {
            if (!string.IsNullOrWhiteSpace(nextSceneName)) SceneManager.LoadScene(nextSceneName);
        }

        public void Retry() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
