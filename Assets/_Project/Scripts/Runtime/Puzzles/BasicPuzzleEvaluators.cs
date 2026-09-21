using System;
using System.Collections.Generic;
using System.Linq;

namespace MythicPuzzle.Runtime
{
    public sealed class TapSelectEvaluator : IPuzzleEvaluator
    {
        private HashSet<string> correctIds;

        public void Initialize(PuzzleDefinition definition) =>
            correctIds = new HashSet<string>(definition.CorrectEntityIds);

        public PuzzleEvaluation Evaluate(PuzzleInputSignal signal)
        {
            if (signal.Kind != PuzzleInputKind.Tap) return PuzzleEvaluation.Ignored;
            return correctIds.Contains(signal.SourceEntityId)
                ? PuzzleEvaluation.Correct
                : PuzzleEvaluation.Incorrect;
        }

        public void Reset() { }
    }

    public sealed class MultiSelectEvaluator : IPuzzleEvaluator
    {
        private IReadOnlyList<string> correctIds;
        private HashSet<string> remaining;

        public void Initialize(PuzzleDefinition definition)
        {
            correctIds = definition.CorrectEntityIds;
            Reset();
        }

        public PuzzleEvaluation Evaluate(PuzzleInputSignal signal)
        {
            if (signal.Kind != PuzzleInputKind.Tap) return PuzzleEvaluation.Ignored;
            if (!remaining.Remove(signal.SourceEntityId)) return PuzzleEvaluation.Incorrect;
            return remaining.Count == 0 ? PuzzleEvaluation.Correct : PuzzleEvaluation.Continue;
        }

        public void Reset() => remaining = new HashSet<string>(correctIds);
    }

    public sealed class SequenceOrderEvaluator : IPuzzleEvaluator
    {
        private IReadOnlyList<string> orderedIds;
        private int index;

        public void Initialize(PuzzleDefinition definition)
        {
            orderedIds = definition.OrderedEntityIds;
            index = 0;
        }

        public PuzzleEvaluation Evaluate(PuzzleInputSignal signal)
        {
            if (signal.Kind != PuzzleInputKind.Tap) return PuzzleEvaluation.Ignored;
            if (index >= orderedIds.Count || signal.SourceEntityId != orderedIds[index])
            {
                return PuzzleEvaluation.Incorrect;
            }

            index++;
            return index == orderedIds.Count ? PuzzleEvaluation.Correct : PuzzleEvaluation.Continue;
        }

        public void Reset() => index = 0;
    }

    public sealed class DragDropEvaluator : IPuzzleEvaluator
    {
        private HashSet<string> validPairs;

        public void Initialize(PuzzleDefinition definition)
        {
            validPairs = new HashSet<string>(
                definition.ValidPairs.Select(pair => Key(pair.sourceId, pair.destinationId)));
        }

        public PuzzleEvaluation Evaluate(PuzzleInputSignal signal)
        {
            if (signal.Kind != PuzzleInputKind.Dropped) return PuzzleEvaluation.Ignored;
            return validPairs.Contains(Key(signal.SourceEntityId, signal.DestinationEntityId))
                ? PuzzleEvaluation.Correct
                : PuzzleEvaluation.Incorrect;
        }

        public void Reset() { }

        private static string Key(string source, string destination) => $"{source}>{destination}";
    }

    public sealed class SwipeDirectionEvaluator : IPuzzleEvaluator
    {
        private SwipeDirection correctDirection;

        public void Initialize(PuzzleDefinition definition) =>
            correctDirection = definition.CorrectSwipeDirection;

        public PuzzleEvaluation Evaluate(PuzzleInputSignal signal)
        {
            if (signal.Kind != PuzzleInputKind.Swipe) return PuzzleEvaluation.Ignored;
            return signal.SwipeDirection == correctDirection
                ? PuzzleEvaluation.Correct
                : PuzzleEvaluation.Incorrect;
        }

        public void Reset() { }
    }

}
