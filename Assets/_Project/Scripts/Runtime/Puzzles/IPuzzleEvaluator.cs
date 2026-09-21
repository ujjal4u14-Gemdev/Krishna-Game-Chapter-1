namespace MythicPuzzle.Runtime
{
    public interface IPuzzleEvaluator
    {
        void Initialize(PuzzleDefinition definition);
        PuzzleEvaluation Evaluate(PuzzleInputSignal signal);
        void Reset();
    }
}

