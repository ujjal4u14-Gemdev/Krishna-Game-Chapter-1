using System;
using System.Collections.Generic;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public enum ActionStepType
    {
        Wait,
        SetActive,
        MoveTo,
        RotateTo,
        ScaleTo,
        PlayAnimation,
        SetExpression,
        SpawnVfx,
        PlayAudio,
        CameraPunch,
        RaiseEvent
    }

    [CreateAssetMenu(menuName = "Mythic Puzzle/Action Sequence", fileName = "SEQ_NewSequence")]
    public sealed class ActionSequenceAsset : ScriptableObject
    {
        [SerializeField] private List<ActionStep> steps = new();
        public IReadOnlyList<ActionStep> Steps => steps;
    }

    [Serializable]
    public struct ActionStep
    {
        public ActionStepType type;
        public string entityId;
        public string stringValue;
        public Vector3 vectorValue;
        public float duration;
        public bool boolValue;
    }
}

