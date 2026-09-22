using System;
using System.Collections.Generic;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    /// <summary>
    /// Replaceable visual bindings for one level. Gameplay refers to SceneEntity IDs while
    /// presentation refers to art-slot IDs, so changing a sprite never changes puzzle rules.
    /// </summary>
    [CreateAssetMenu(menuName = "Mythic Puzzle/Presentation/Level Art Set", fileName = "ART_NewLevel")]
    public sealed class LevelArtSet : ScriptableObject
    {
        [SerializeField] private string levelId;
        [SerializeField] private List<LevelArtBinding> bindings = new();

        public string LevelId => levelId;
        public IReadOnlyList<LevelArtBinding> Bindings => bindings;

        public bool TryGet(string slotId, out LevelArtBinding binding)
        {
            foreach (var candidate in bindings)
            {
                if (string.Equals(candidate.slotId, slotId, StringComparison.Ordinal))
                {
                    binding = candidate;
                    return true;
                }
            }

            binding = default;
            return false;
        }
    }

    [Serializable]
    public struct LevelArtBinding
    {
        public string slotId;
        public Sprite sprite;
        public Color tint;
        public bool useNativeSize;
    }
}
