using System.Collections.Generic;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    [CreateAssetMenu(menuName = "Mythic Puzzle/Level Definition", fileName = "LVL_NewLevel")]
    public sealed class LevelDefinition : ScriptableObject
    {
        [SerializeField] private string levelId;
        [SerializeField] private string sceneName;
        [SerializeField] private List<PuzzleDefinition> puzzles = new();
        [SerializeField] private ActionSequenceAsset completionSequence;

        public string LevelId => levelId;
        public string SceneName => sceneName;
        public IReadOnlyList<PuzzleDefinition> Puzzles => puzzles;
        public ActionSequenceAsset CompletionSequence => completionSequence;
    }
}

