using MythicPuzzle.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace MythicPuzzle.UI
{
    /// <summary>Owns only HUD presentation. It never decides whether a puzzle is correct.</summary>
    public sealed class GameHUDController : MonoBehaviour
    {
        [SerializeField] private PuzzleDirector puzzleDirector;
        [SerializeField] private Text levelLabel;
        [SerializeField] private Text progressLabel;
        [SerializeField] private Button pauseButton;
        [SerializeField] private UIPanel pausePanel;

        private int puzzleCount;

        private void OnEnable()
        {
            if (puzzleDirector != null)
            {
                puzzleDirector.PuzzleChanged += OnPuzzleChanged;
                Configure(
                    puzzleDirector.Level == null ? "Level" : $"Level {puzzleDirector.Level.LevelNumber}",
                    puzzleDirector.PuzzleCount);
            }
            if (pauseButton != null) pauseButton.onClick.AddListener(OpenPause);
        }

        private void OnDisable()
        {
            if (puzzleDirector != null) puzzleDirector.PuzzleChanged -= OnPuzzleChanged;
            if (pauseButton != null) pauseButton.onClick.RemoveListener(OpenPause);
        }

        public void Configure(string levelName, int totalPuzzles)
        {
            if (levelLabel != null) levelLabel.text = levelName;
            puzzleCount = Mathf.Max(1, totalPuzzles);
            OnPuzzleChanged(0);
        }

        private void OnPuzzleChanged(int index)
        {
            if (progressLabel != null) progressLabel.text = $"{index + 1}/{puzzleCount}";
        }

        private void OpenPause()
        {
            pausePanel?.Show();
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            pausePanel?.Hide();
        }
    }
}
