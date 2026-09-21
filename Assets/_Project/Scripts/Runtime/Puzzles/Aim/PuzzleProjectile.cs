using UnityEngine;

namespace MythicPuzzle.Runtime
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PuzzleProjectile : MonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float maximumLifetime = 5f;
        private Rigidbody2D body;
        private PuzzleInputRouter inputRouter;
        private bool resolved;

        private void Awake() => body = GetComponent<Rigidbody2D>();

        public void Launch(Vector2 velocity, PuzzleInputRouter router)
        {
            inputRouter = router;
            body.linearVelocity = velocity;
            Invoke(nameof(ResolveMiss), maximumLifetime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (resolved) return;
            resolved = true;

            var target = collision.collider.GetComponentInParent<ProjectileTarget>();
            if (target != null)
            {
                var screenPosition = Camera.main == null
                    ? Vector2.zero
                    : (Vector2)Camera.main.WorldToScreenPoint(transform.position);
                inputRouter?.RaiseProjectileHit(target.TargetId, screenPosition);
            }
            else
            {
                inputRouter?.RaiseProjectileHit("__miss__", Vector2.zero);
            }

            Destroy(gameObject);
        }

        private void ResolveMiss()
        {
            if (resolved) return;
            resolved = true;
            inputRouter?.RaiseProjectileHit("__miss__", Vector2.zero);
            Destroy(gameObject);
        }
    }
}
