using System.Collections.Generic;

namespace MythicPuzzle.Runtime
{
    /// <summary>Evaluates the two-option decisions and route choices used in Chapter 1.</summary>
    public sealed class BinaryChoiceEvaluator : IPuzzleEvaluator
    {
        private HashSet<string> correctChoiceIds;

        public void Initialize(PuzzleDefinition definition) =>
            correctChoiceIds = new HashSet<string>(definition.CorrectEntityIds);

        public PuzzleEvaluation Evaluate(PuzzleInputSignal signal)
        {
            if (signal.Kind != PuzzleInputKind.Tap) return PuzzleEvaluation.Ignored;
            return correctChoiceIds.Contains(signal.SourceEntityId)
                ? PuzzleEvaluation.Correct
                : PuzzleEvaluation.Incorrect;
        }

        public void Reset() { }
    }
}
