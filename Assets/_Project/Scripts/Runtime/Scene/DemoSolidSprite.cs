using UnityEngine;

namespace MythicPuzzle.Runtime
{
    /// <summary>Creates a one-pixel sprite at runtime for dependency-free greybox scenes.</summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class DemoSolidSprite : MonoBehaviour
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private int sortingOrder;

        private Texture2D texture;
        private Sprite sprite;

        private void Awake()
        {
            texture = new Texture2D(1, 1) { name = $"{name}_GreyboxTexture" };
            texture.SetPixel(0, 0, Color.white);
            texture.Apply();
            sprite = Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);

            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.color = color;
            spriteRenderer.sortingOrder = sortingOrder;
        }

        private void OnDestroy()
        {
            if (sprite != null) Destroy(sprite);
            if (texture != null) Destroy(texture);
        }
    }
}
