using System;
using System.Collections;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public sealed class ActionSequenceRunner : MonoBehaviour
    {
        [SerializeField] private SceneRegistry sceneRegistry;
        public event Action<string> EventRaised;

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

            if (step.type == ActionStepType.RaiseEvent)
            {
                EventRaised?.Invoke(step.stringValue);
                yield break;
            }

            if (sceneRegistry == null || !sceneRegistry.TryGet(step.entityId, out var entity))
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
                case ActionStepType.SpawnVfx:
                    entity.GetComponentInChildren<ParticleSystem>(true)?.Play(true);
                    break;
                case ActionStepType.PlayAudio:
                    entity.GetComponent<AudioSource>()?.Play();
                    break;
                case ActionStepType.CameraPunch:
                    yield return Punch(entity.transform, step.vectorValue, step.duration);
                    break;
            }
        }

        private static IEnumerator Punch(Transform target, Vector3 strength, float duration)
        {
            var origin = target.localPosition;
            var punch = strength == Vector3.zero ? new Vector3(0.12f, 0.12f, 0f) : strength;
            var elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var remaining = 1f - Mathf.Clamp01(elapsed / Mathf.Max(duration, 0.001f));
                target.localPosition = origin + Vector3.Scale(UnityEngine.Random.insideUnitSphere, punch) * remaining;
                yield return null;
            }

            target.localPosition = origin;
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
