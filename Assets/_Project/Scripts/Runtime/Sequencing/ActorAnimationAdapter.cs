using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public abstract class ActorAnimationAdapter : MonoBehaviour, IActorAnimationAdapter
    {
        public abstract void Play(string animationName);
        public abstract void SetExpression(string expressionName);
    }
}
