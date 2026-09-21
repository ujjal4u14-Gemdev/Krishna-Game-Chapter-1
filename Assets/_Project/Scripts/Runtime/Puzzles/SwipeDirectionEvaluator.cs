namespace MythicPuzzle.Runtime
{
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
