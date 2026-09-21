using System;

namespace MythicPuzzle.Runtime
{
    public static class PuzzleEvaluatorRegistry
    {
        public static IPuzzleEvaluator Create(PuzzleType type)
        {
            return type switch
            {
                PuzzleType.EraseReveal => new EraseRevealEvaluator(),
                PuzzleType.BinaryChoice => new BinaryChoiceEvaluator(),
                PuzzleType.AimAndShoot => new AimAndShootEvaluator(),
                PuzzleType.TapSelect => new TapSelectEvaluator(),
                PuzzleType.MultiSelect => new MultiSelectEvaluator(),
                PuzzleType.SequenceOrder => new SequenceOrderEvaluator(),
                PuzzleType.DragDrop => new DragDropEvaluator(),
                PuzzleType.SwipeDirection => new SwipeDirectionEvaluator(),
                _ => throw new NotSupportedException($"Puzzle type {type} is not implemented yet.")
            };
        }
    }
}
