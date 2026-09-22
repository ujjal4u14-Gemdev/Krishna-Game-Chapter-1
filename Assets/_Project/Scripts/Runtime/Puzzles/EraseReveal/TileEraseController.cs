using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace MythicPuzzle.Runtime
{
    /// <summary>
    /// Low-cost eraser for mobile. Split the removable artwork into small collider tiles and
    /// place this component on a pointer-receiving object above them.
    /// </summary>
    [RequireComponent(typeof(SceneEntity))]
    public sealed class TileEraseController : MonoBehaviour,
        IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private PuzzleInputRouter inputRouter;
        [SerializeField] private PuzzleDirector puzzleDirector;
        [SerializeField] private Camera worldCamera;
        [SerializeField] private LayerMask erasableLayers = ~0;
        [SerializeField, Min(0.01f)] private float brushRadius = 0.2f;
        [SerializeField] private List<ErasableTile> tiles = new();
        [SerializeField] private bool useDirectPointerInput;

        private readonly HashSet<ErasableTile> erasedTiles = new();
        private SceneEntity sceneEntity;
        private bool strokeActive;

        private void Awake()
        {
            sceneEntity = GetComponent<SceneEntity>();
            if (worldCamera == null) worldCamera = Camera.main;
            if (tiles.Count == 0) tiles.AddRange(GetComponentsInChildren<ErasableTile>(true));
        }

        private void OnEnable()
        {
            if (puzzleDirector != null) puzzleDirector.StateChanged += OnPuzzleStateChanged;
        }

        private void OnDisable()
        {
            if (puzzleDirector != null) puzzleDirector.StateChanged -= OnPuzzleStateChanged;
            strokeActive = false;
        }

        private void Update()
        {
            if (!useDirectPointerInput || !CanInteract()) return;
            var pointer = Pointer.current;
            if (pointer == null) return;

            if (pointer.press.wasPressedThisFrame) strokeActive = true;
            if (strokeActive && pointer.press.isPressed) EraseAt(pointer.position.ReadValue());
            if (pointer.press.wasReleasedThisFrame) strokeActive = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (useDirectPointerInput || !CanInteract()) return;
            strokeActive = true;
            EraseAt(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!useDirectPointerInput && strokeActive) EraseAt(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!useDirectPointerInput) strokeActive = false;
        }

        public void ResetTiles()
        {
            foreach (var tile in tiles)
            {
                if (tile != null) tile.Restore();
            }

            erasedTiles.Clear();
            ReportProgress();
        }

        private void EraseAt(Vector2 screenPosition)
        {
            if (worldCamera == null || tiles.Count == 0) return;

            var world = worldCamera.ScreenToWorldPoint(screenPosition);
            var hits = Physics2D.OverlapCircleAll((Vector2)world, brushRadius, erasableLayers);
            var changed = false;

            foreach (var hit in hits)
            {
                var tile = hit.GetComponent<ErasableTile>();
                if (tile == null || !tiles.Contains(tile) || !tile.Erase()) continue;
                erasedTiles.Add(tile);
                changed = true;
            }

            if (changed) ReportProgress();
        }

        private void ReportProgress()
        {
            var progress = tiles.Count == 0 ? 0f : (float)erasedTiles.Count / tiles.Count;
            inputRouter?.RaiseEraseProgress(sceneEntity.EntityId, progress);
        }

        private void OnPuzzleStateChanged(PuzzleState state)
        {
            if (state == PuzzleState.Resetting) ResetTiles();
        }

        private bool CanInteract() =>
            puzzleDirector == null || puzzleDirector.State == PuzzleState.AwaitingInput;
    }
}
