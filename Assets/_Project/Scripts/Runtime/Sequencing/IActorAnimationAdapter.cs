namespace MythicPuzzle.Runtime
{
    public interface IActorAnimationAdapter
    {
        void Play(string animationName);
        void SetExpression(string expressionName);
    }
}
