using System;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public sealed class PuzzleInputRouter : MonoBehaviour
    {
        public event Action<PuzzleInputSignal> SignalRaised;

        public bool InputEnabled { get; set; }

        public void Raise(PuzzleInputSignal signal)
        {
            if (InputEnabled)
            {
                SignalRaised?.Invoke(signal);
            }
        }

        public void RaiseTap(string entityId, Vector2 screenPosition)
        {
            Raise(new PuzzleInputSignal(PuzzleInputKind.Tap, entityId, screenPosition: screenPosition));
        }

        public void RaiseEraseProgress(string maskId, float normalizedProgress)
        {
            Raise(new PuzzleInputSignal(
                PuzzleInputKind.EraseProgress,
                maskId,
                scalarValue: Mathf.Clamp01(normalizedProgress)));
        }

        public void RaiseProjectileHit(string targetId, Vector2 screenPosition)
        {
            Raise(new PuzzleInputSignal(
                PuzzleInputKind.ProjectileHit,
                targetId,
                screenPosition: screenPosition));
        }

        public void RaiseDrop(string sourceId, string destinationId)
        {
            Raise(new PuzzleInputSignal(PuzzleInputKind.Dropped, sourceId, destinationId));
        }

        public void RaiseSwipe(SwipeDirection direction)
        {
            Raise(new PuzzleInputSignal(PuzzleInputKind.Swipe, string.Empty, swipeDirection: direction));
        }
    }
}
