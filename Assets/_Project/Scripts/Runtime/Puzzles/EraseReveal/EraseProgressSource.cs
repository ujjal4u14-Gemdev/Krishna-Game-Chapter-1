using UnityEngine;

namespace MythicPuzzle.Runtime
{
    /// <summary>
    /// Bridge for any eraser implementation (RenderTexture, shader mask, sprite tiles, etc.).
    /// The visual implementation reports normalized coverage; puzzle rules remain independent.
    /// </summary>
    [RequireComponent(typeof(SceneEntity))]
    public sealed class EraseProgressSource : MonoBehaviour
    {
        [SerializeField] private PuzzleInputRouter inputRouter;
        private SceneEntity sceneEntity;

        private void Awake() => sceneEntity = GetComponent<SceneEntity>();

        public void ReportProgress(float normalizedProgress)
        {
            inputRouter?.RaiseEraseProgress(sceneEntity.EntityId, normalizedProgress);
        }
    }
}
