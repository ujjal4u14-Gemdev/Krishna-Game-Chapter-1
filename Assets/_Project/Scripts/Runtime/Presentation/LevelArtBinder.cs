using System.Collections.Generic;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    /// <summary>Applies a LevelArtSet to all stable SpriteArtSlot components in a scene.</summary>
    public sealed class LevelArtBinder : MonoBehaviour
    {
        [SerializeField] private LevelArtSet artSet;
        [SerializeField] private List<SpriteArtSlot> slots = new();

        public LevelArtSet ArtSet => artSet;

        private void Awake()
        {
            if (slots.Count == 0) slots.AddRange(GetComponentsInChildren<SpriteArtSlot>(true));
            Bind();
        }

        [ContextMenu("Bind Art Set")]
        public void Bind()
        {
            if (artSet == null) return;

            foreach (var slot in slots)
            {
                if (slot != null && artSet.TryGet(slot.SlotId, out var binding)) slot.Apply(binding);
            }
        }
    }
}
