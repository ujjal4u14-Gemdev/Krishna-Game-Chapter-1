using UnityEngine;

namespace MythicPuzzle.Runtime
{
    [DisallowMultipleComponent]
    public sealed class ErasableTile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer tileRenderer;
        [SerializeField] private SpriteMask tileMask;
        [SerializeField] private Collider2D tileCollider;

        private static Texture2D sharedMaskTexture;
        private static Sprite sharedMaskSprite;
        private static int sharedMaskUsers;
        private bool usesSharedMask;

        public bool IsErased { get; private set; }

        private void Reset()
        {
            tileRenderer = GetComponent<SpriteRenderer>();
            tileMask = GetComponent<SpriteMask>();
            tileCollider = GetComponent<Collider2D>();
        }

        private void Awake()
        {
            if (tileRenderer == null) tileRenderer = GetComponent<SpriteRenderer>();
            if (tileMask == null) tileMask = GetComponent<SpriteMask>();
            if (tileCollider == null) tileCollider = GetComponent<Collider2D>();

            if (tileMask != null && tileMask.sprite == null)
            {
                EnsureSharedMask();
                tileMask.sprite = sharedMaskSprite;
                sharedMaskUsers++;
                usesSharedMask = true;
            }
        }

        public bool Erase()
        {
            if (IsErased) return false;

            IsErased = true;
            if (tileRenderer != null) tileRenderer.enabled = false;
            if (tileMask != null) tileMask.enabled = false;
            if (tileCollider != null) tileCollider.enabled = false;
            return true;
        }

        public void Restore()
        {
            IsErased = false;
            if (tileRenderer != null) tileRenderer.enabled = true;
            if (tileMask != null) tileMask.enabled = true;
            if (tileCollider != null) tileCollider.enabled = true;
        }

        private void OnDestroy()
        {
            if (!usesSharedMask) return;
            sharedMaskUsers = Mathf.Max(0, sharedMaskUsers - 1);
            if (sharedMaskUsers > 0) return;

            if (sharedMaskSprite != null) Destroy(sharedMaskSprite);
            if (sharedMaskTexture != null) Destroy(sharedMaskTexture);
            sharedMaskSprite = null;
            sharedMaskTexture = null;
        }

        private static void EnsureSharedMask()
        {
            if (sharedMaskSprite != null) return;
            sharedMaskTexture = new Texture2D(1, 1) { name = "SharedEraseMaskTexture" };
            sharedMaskTexture.SetPixel(0, 0, Color.white);
            sharedMaskTexture.Apply();
            sharedMaskSprite = Sprite.Create(
                sharedMaskTexture,
                new Rect(0, 0, 1, 1),
                new Vector2(0.5f, 0.5f),
                1f);
        }
    }
}
