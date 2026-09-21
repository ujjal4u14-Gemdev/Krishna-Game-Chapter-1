using System.Collections.Generic;

namespace MythicPuzzle.Runtime
{
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
}
