using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace MythicPuzzle.Runtime
{
    [RequireComponent(typeof(SceneEntity))]
    public sealed class PuzzleTapTarget : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private PuzzleInputRouter inputRouter;
        [SerializeField] private bool useDirectPointerInput;
        private SceneEntity sceneEntity;
        private Collider2D targetCollider;

        private void Awake()
        {
            sceneEntity = GetComponent<SceneEntity>();
            targetCollider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (!useDirectPointerInput || inputRouter == null || !inputRouter.InputEnabled) return;
            var pointer = Pointer.current;
            if (pointer == null || !pointer.press.wasReleasedThisFrame || targetCollider == null) return;

            var camera = Camera.main;
            if (camera == null) return;
            var world = (Vector2)camera.ScreenToWorldPoint(pointer.position.ReadValue());
            if (targetCollider.OverlapPoint(world)) Raise(pointer.position.ReadValue());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!useDirectPointerInput) Raise(eventData.position);
        }

        private void Raise(Vector2 screenPosition) =>
            inputRouter?.RaiseTap(sceneEntity.EntityId, screenPosition);
    }
}
