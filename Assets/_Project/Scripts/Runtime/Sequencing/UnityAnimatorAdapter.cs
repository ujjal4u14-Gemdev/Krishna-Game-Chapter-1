using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public sealed class UnityAnimatorAdapter : ActorAnimationAdapter
    {
        [SerializeField] private Animator animator;

        public override void Play(string animationName)
        {
            if (animator != null && !string.IsNullOrWhiteSpace(animationName))
            {
                animator.CrossFade(animationName, 0.1f);
            }
        }

        public override void SetExpression(string expressionName)
        {
            if (animator != null && !string.IsNullOrWhiteSpace(expressionName))
            {
                animator.SetTrigger(expressionName);
            }
        }
    }
}

