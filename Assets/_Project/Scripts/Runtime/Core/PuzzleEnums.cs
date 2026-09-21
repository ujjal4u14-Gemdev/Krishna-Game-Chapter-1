namespace MythicPuzzle.Runtime
{
    public enum PuzzleType
    {
        EraseReveal,
        BinaryChoice,
        AimAndShoot,
        TapSelect,
        MultiSelect,
        DragDrop,
        SequenceOrder,
        SwipeDirection,
        HiddenObject,
        MatchPairs,
        RouteChoice,
        CombineItems,
        StateToggle,
        TimingTap,
        HoldCharge,
        TracePath,
        CountCompare,
        MemoryRecall,
        PhysicsBalance,
        ResourceBattle,
        DialogueChoice
    }

    public enum PuzzleState
    {
        None,
        Intro,
        AwaitingInput,
        Resolving,
        Success,
        Failure,
        Resetting,
        Complete
    }

    public enum PuzzleInputKind
    {
        Tap,
        EraseProgress,
        ProjectileHit,
        DragStarted,
        DragEnded,
        Dropped,
        Swipe,
        HoldStarted,
        HoldReleased,
        TraceCompleted
    }

    public enum PuzzleEvaluation
    {
        Ignored,
        Continue,
        Correct,
        Incorrect
    }

    public enum SwipeDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}
