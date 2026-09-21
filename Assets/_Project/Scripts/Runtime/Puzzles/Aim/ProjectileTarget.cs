using UnityEngine;

namespace MythicPuzzle.Runtime
{
    /// <summary>Converts a scene-specific projectile collision into an engine-level target hit.</summary>
    [RequireComponent(typeof(SceneEntity))]
    public sealed class ProjectileTarget : MonoBehaviour
    {
        [SerializeField] private PuzzleInputRouter inputRouter;
        private SceneEntity sceneEntity;

        private void Awake() => sceneEntity = GetComponent<SceneEntity>();

        public void ResolveHit(Vector2 screenPosition)
        {
            inputRouter.RaiseProjectileHit(sceneEntity.EntityId, screenPosition);
        }
    }
}
