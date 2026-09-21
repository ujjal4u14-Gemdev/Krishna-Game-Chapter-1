using System.Collections.Generic;

namespace MythicPuzzle.Runtime
{
    /// <summary>Completes after the authored mask reaches its configured erase threshold.</summary>
    public sealed class EraseRevealEvaluator : IPuzzleEvaluator
    {
        private HashSet<string> validMaskIds;
        private float threshold;

        public void Initialize(PuzzleDefinition definition)
        {
            validMaskIds = new HashSet<string>(definition.CorrectEntityIds);
            threshold = definition.RequiredProgress;
        }

        public PuzzleEvaluation Evaluate(PuzzleInputSignal signal)
        {
            if (signal.Kind != PuzzleInputKind.EraseProgress) return PuzzleEvaluation.Ignored;
            if (!validMaskIds.Contains(signal.SourceEntityId)) return PuzzleEvaluation.Incorrect;
            return signal.ScalarValue >= threshold
                ? PuzzleEvaluation.Correct
                : PuzzleEvaluation.Continue;
        }

        public void Reset() { }
    }
}
