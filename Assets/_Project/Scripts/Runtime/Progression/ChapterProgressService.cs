using System;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    /// <summary>Persists completion without coupling puzzle rules to storage.</summary>
    public sealed class ChapterProgressService : MonoBehaviour
    {
        [SerializeField] private PuzzleDirector puzzleDirector;
        [SerializeField] private JsonProgressStore progressStore;

        public PlayerProgressData Current { get; private set; }
        public event Action<PlayerProgressData> ProgressChanged;

        private void Awake()
        {
            if (progressStore != null) Current = progressStore.Load();
            Current ??= new PlayerProgressData();
        }

        private void OnEnable()
        {
            if (puzzleDirector != null) puzzleDirector.LevelCompleted += OnLevelCompleted;
        }

        private void OnDisable()
        {
            if (puzzleDirector != null) puzzleDirector.LevelCompleted -= OnLevelCompleted;
        }

        private void OnLevelCompleted(LevelDefinition level)
        {
            if (!Current.completedLevelIds.Contains(level.LevelId))
            {
                Current.completedLevelIds.Add(level.LevelId);
            }

            Current.highestUnlockedLevel = Mathf.Max(
                Current.highestUnlockedLevel,
                level.LevelNumber + 1);
            progressStore?.Save(Current);
            ProgressChanged?.Invoke(Current);
        }
    }
}
