using System.Collections.Generic;

namespace MythicPuzzle.Runtime
{
    /// <summary>Evaluates the resolved projectile hit; aiming physics stays in the scene adapter.</summary>
    public sealed class AimAndShootEvaluator : IPuzzleEvaluator
    {
        private HashSet<string> successTargets;

        public void Initialize(PuzzleDefinition definition) =>
            successTargets = new HashSet<string>(definition.CorrectEntityIds);

        public PuzzleEvaluation Evaluate(PuzzleInputSignal signal)
        {
            if (signal.Kind != PuzzleInputKind.ProjectileHit) return PuzzleEvaluation.Ignored;
            return successTargets.Contains(signal.SourceEntityId)
                ? PuzzleEvaluation.Correct
                : PuzzleEvaluation.Incorrect;
        }

        public void Reset() { }
    }
}
