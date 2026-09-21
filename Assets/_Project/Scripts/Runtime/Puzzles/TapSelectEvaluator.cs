using System.Collections.Generic;

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
}
