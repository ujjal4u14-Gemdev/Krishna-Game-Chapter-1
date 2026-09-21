using System.Collections.Generic;
using System.Linq;

namespace MythicPuzzle.Runtime
{
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
}
