using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public readonly struct PuzzleInputSignal
    {
        public PuzzleInputSignal(
            PuzzleInputKind kind,
            string sourceEntityId,
            string destinationEntityId = "",
            SwipeDirection swipeDirection = SwipeDirection.None,
            Vector2 screenPosition = default,
            float scalarValue = 0f)
        {
            Kind = kind;
            SourceEntityId = sourceEntityId;
            DestinationEntityId = destinationEntityId;
            SwipeDirection = swipeDirection;
            ScreenPosition = screenPosition;
            ScalarValue = scalarValue;
        }

        public PuzzleInputKind Kind { get; }
        public string SourceEntityId { get; }
        public string DestinationEntityId { get; }
        public SwipeDirection SwipeDirection { get; }
        public Vector2 ScreenPosition { get; }
        public float ScalarValue { get; }
    }
}

