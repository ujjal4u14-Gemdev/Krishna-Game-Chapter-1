using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public sealed class SceneEntity : MonoBehaviour
    {
        [SerializeField] private string entityId;

        public string EntityId => entityId;
    }
}

