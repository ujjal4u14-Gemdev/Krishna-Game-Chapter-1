using System.Collections.Generic;

namespace MythicPuzzle.Runtime
{
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
}
