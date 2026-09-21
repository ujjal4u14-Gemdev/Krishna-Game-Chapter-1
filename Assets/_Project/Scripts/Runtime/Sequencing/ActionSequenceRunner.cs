using System;
using System.Collections;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public sealed class ActionSequenceRunner : MonoBehaviour
    {
        [SerializeField] private SceneRegistry sceneRegistry;

        public IEnumerator Run(ActionSequenceAsset sequence)
        {
            if (sequence == null) yield break;

            foreach (var step in sequence.Steps)
            {
                yield return Execute(step);
            }
        }

        private IEnumerator Execute(ActionStep step)
        {
            if (step.type == ActionStepType.Wait)
            {
                yield return new WaitForSeconds(step.duration);
                yield break;
            }

            if (!sceneRegistry.TryGet(step.entityId, out var entity))
            {
                Debug.LogWarning($"Sequence entity not found: {step.entityId}");
                yield break;
            }

            switch (step.type)
            {
                case ActionStepType.SetActive:
                    entity.gameObject.SetActive(step.boolValue);
                    break;
                case ActionStepType.MoveTo:
                    yield return TweenVector(
                        entity.transform.position,
                        step.vectorValue,
                        step.duration,
                        value => entity.transform.position = value);
                    break;
                case ActionStepType.RotateTo:
                    yield return TweenVector(
                        entity.transform.eulerAngles,
                        step.vectorValue,
                        step.duration,
                        value => entity.transform.eulerAngles = value);
                    break;
                case ActionStepType.ScaleTo:
                    yield return TweenVector(
                        entity.transform.localScale,
                        step.vectorValue,
                        step.duration,
                        value => entity.transform.localScale = value);
                    break;
                case ActionStepType.PlayAnimation:
                    entity.GetComponent<ActorAnimationAdapter>()?.Play(step.stringValue);
                    break;
                case ActionStepType.SetExpression:
                    entity.GetComponent<ActorAnimationAdapter>()?.SetExpression(step.stringValue);
                    break;
                default:
                    Debug.Log($"Action adapter pending for {step.type}: {step.stringValue}");
                    break;
            }
        }

        private static IEnumerator TweenVector(
            Vector3 from,
            Vector3 to,
            float duration,
            Action<Vector3> apply)
        {
            if (duration <= 0f)
            {
                apply(to);
                yield break;
            }

            var elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                apply(Vector3.LerpUnclamped(from, to, Mathf.SmoothStep(0f, 1f, t)));
                yield return null;
            }

            apply(to);
        }
    }
}
