using UnityEngine;

namespace MythicPuzzle.Runtime
{
    /// <summary>
    /// Stable replacement point for final art. If no sprite is supplied, a colored fallback
    /// is generated at runtime so every authored level remains playable before art arrives.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class SpriteArtSlot : MonoBehaviour
    {
        [SerializeField] private string slotId;
        [SerializeField] private SpriteRenderer targetRenderer;
        [SerializeField] private Color fallbackColor = Color.white;
        [SerializeField] private int sortingOrder;

        private Texture2D fallbackTexture;
        private Sprite fallbackSprite;

        public string SlotId => slotId;

        private void Awake()
        {
            if (targetRenderer == null) targetRenderer = GetComponent<SpriteRenderer>();
            targetRenderer.sortingOrder = sortingOrder;
            if (targetRenderer.sprite == null) CreateFallback();
        }

        public void Apply(LevelArtBinding binding)
        {
            if (targetRenderer == null) targetRenderer = GetComponent<SpriteRenderer>();
            if (binding.sprite == null) return;

            targetRenderer.sprite = binding.sprite;
            targetRenderer.color = binding.tint.a <= 0f ? Color.white : binding.tint;
            if (binding.useNativeSize) targetRenderer.transform.localScale = Vector3.one;
        }

        private void CreateFallback()
        {
            fallbackTexture = new Texture2D(1, 1) { name = $"{name}_FallbackTexture" };
            fallbackTexture.SetPixel(0, 0, Color.white);
            fallbackTexture.Apply();
            fallbackSprite = Sprite.Create(
                fallbackTexture,
                new Rect(0, 0, 1, 1),
                new Vector2(0.5f, 0.5f),
                1f);
            targetRenderer.sprite = fallbackSprite;
            targetRenderer.color = fallbackColor;
        }

        private void OnDestroy()
        {
            if (fallbackSprite != null) Destroy(fallbackSprite);
            if (fallbackTexture != null) Destroy(fallbackTexture);
        }
    }
}
