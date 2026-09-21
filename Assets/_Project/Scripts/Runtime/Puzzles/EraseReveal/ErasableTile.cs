using UnityEngine;

namespace MythicPuzzle.Runtime
{
    [DisallowMultipleComponent]
    public sealed class ErasableTile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer tileRenderer;
        [SerializeField] private Collider2D tileCollider;

        public bool IsErased { get; private set; }

        private void Reset()
        {
            tileRenderer = GetComponent<SpriteRenderer>();
            tileCollider = GetComponent<Collider2D>();
        }

        public bool Erase()
        {
            if (IsErased) return false;

            IsErased = true;
            if (tileRenderer != null) tileRenderer.enabled = false;
            if (tileCollider != null) tileCollider.enabled = false;
            return true;
        }

        public void Restore()
        {
            IsErased = false;
            if (tileRenderer != null) tileRenderer.enabled = true;
            if (tileCollider != null) tileCollider.enabled = true;
        }
    }
}
