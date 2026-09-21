using System.Collections.Generic;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public sealed class SceneRegistry : MonoBehaviour
    {
        private readonly Dictionary<string, SceneEntity> entities = new();

        private void Awake()
        {
            entities.Clear();
            foreach (var entity in GetComponentsInChildren<SceneEntity>(true))
            {
                if (!string.IsNullOrWhiteSpace(entity.EntityId))
                {
                    entities[entity.EntityId] = entity;
                }
            }
        }

        public bool TryGet(string entityId, out SceneEntity entity) =>
            entities.TryGetValue(entityId, out entity);
    }
}

