using System.Collections.Generic;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    [CreateAssetMenu(menuName = "Mythic Puzzle/Level Definition", fileName = "LVL_NewLevel")]
    public sealed class LevelDefinition : ScriptableObject
    {
        [SerializeField] private string levelId;
        [SerializeField, Min(1)] private int levelNumber = 1;
        [SerializeField] private string sceneName;
        [SerializeField] private List<PuzzleDefinition> puzzles = new();
        [SerializeField] private ActionSequenceAsset completionSequence;

        public string LevelId => levelId;
        public int LevelNumber => levelNumber;
        public string SceneName => sceneName;
        public IReadOnlyList<PuzzleDefinition> Puzzles => puzzles;
        public ActionSequenceAsset CompletionSequence => completionSequence;
    }
}
