using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace MythicPuzzle.Runtime
{
    public sealed class AimAndShootController : MonoBehaviour,
        IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private PuzzleInputRouter inputRouter;
        [SerializeField] private PuzzleDirector puzzleDirector;
        [SerializeField] private Camera worldCamera;
        [SerializeField] private Transform launchPoint;
        [SerializeField] private PuzzleProjectile projectilePrefab;
        [SerializeField] private LineRenderer trajectoryLine;
        [SerializeField, Min(0.1f)] private float launchPower = 6f;
        [SerializeField, Min(0.1f)] private float maxDragWorldUnits = 3f;
        [SerializeField, Range(4, 40)] private int previewPointCount = 18;
        [SerializeField, Min(0.01f)] private float previewTimeStep = 0.08f;
        [SerializeField] private bool useDirectPointerInput;

        private bool aiming;
        private bool shotInFlight;
        private Vector2 currentVelocity;

        private void Awake()
        {
            if (worldCamera == null) worldCamera = Camera.main;
            SetTrajectoryVisible(false);
        }

        private void OnEnable()
        {
            if (puzzleDirector != null) puzzleDirector.StateChanged += OnPuzzleStateChanged;
        }

        private void OnDisable()
        {
            if (puzzleDirector != null) puzzleDirector.StateChanged -= OnPuzzleStateChanged;
            aiming = false;
            SetTrajectoryVisible(false);
        }

        private void Update()
        {
            if (!useDirectPointerInput) return;
            var pointer = Pointer.current;
            if (pointer == null) return;
            var position = pointer.position.ReadValue();

            if (pointer.press.wasPressedThisFrame) BeginAim(position);
            if (aiming && pointer.press.isPressed) UpdateAim(position);
            if (pointer.press.wasReleasedThisFrame) ReleaseShot(position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!useDirectPointerInput) BeginAim(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!useDirectPointerInput && aiming) UpdateAim(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!useDirectPointerInput) ReleaseShot(eventData.position);
        }

        private void BeginAim(Vector2 screenPosition)
        {
            if (!CanInteract() || shotInFlight || worldCamera == null || launchPoint == null) return;
            aiming = true;
            UpdateAim(screenPosition);
        }

        private void ReleaseShot(Vector2 screenPosition)
        {
            if (!aiming) return;
            UpdateAim(screenPosition);
            aiming = false;
            SetTrajectoryVisible(false);

            if (projectilePrefab == null || currentVelocity.sqrMagnitude < 0.01f) return;
            var projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
            projectile.Launch(currentVelocity, inputRouter);
            shotInFlight = true;
        }

        public void ResetShot()
        {
            shotInFlight = false;
            aiming = false;
            currentVelocity = Vector2.zero;
            SetTrajectoryVisible(false);
        }

        private void UpdateAim(Vector2 screenPosition)
        {
            var pointerWorld = (Vector2)worldCamera.ScreenToWorldPoint(screenPosition);
            var drag = Vector2.ClampMagnitude((Vector2)launchPoint.position - pointerWorld, maxDragWorldUnits);
            currentVelocity = drag * launchPower;
            DrawTrajectory(currentVelocity);
        }

        private void DrawTrajectory(Vector2 velocity)
        {
            if (trajectoryLine == null) return;

            trajectoryLine.positionCount = previewPointCount;
            var origin = (Vector2)launchPoint.position;
            for (var index = 0; index < previewPointCount; index++)
            {
                var time = index * previewTimeStep;
                var position = origin + velocity * time + 0.5f * Physics2D.gravity * time * time;
                trajectoryLine.SetPosition(index, position);
            }

            SetTrajectoryVisible(true);
        }

        private void SetTrajectoryVisible(bool visible)
        {
            if (trajectoryLine != null) trajectoryLine.enabled = visible;
        }

        private void OnPuzzleStateChanged(PuzzleState state)
        {
            if (state is PuzzleState.Resetting or PuzzleState.AwaitingInput) ResetShot();
            if (state is PuzzleState.Success or PuzzleState.Complete) SetTrajectoryVisible(false);
        }

        private bool CanInteract() =>
            puzzleDirector == null || puzzleDirector.State == PuzzleState.AwaitingInput;
    }
}
