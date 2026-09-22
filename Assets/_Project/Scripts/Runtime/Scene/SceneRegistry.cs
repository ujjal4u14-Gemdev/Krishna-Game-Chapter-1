using System.Collections.Generic;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public sealed class SceneRegistry : MonoBehaviour
    {
        private readonly Dictionary<string, SceneEntity> entities = new();

        private void Awake() => Rebuild();

        public void Rebuild()
        {
            entities.Clear();
            var scene = gameObject.scene;
            if (!scene.IsValid()) return;
            foreach (var root in scene.GetRootGameObjects())
            {
                foreach (var entity in root.GetComponentsInChildren<SceneEntity>(true))
                {
                    if (string.IsNullOrWhiteSpace(entity.EntityId)) continue;
                    if (entities.ContainsKey(entity.EntityId))
                        Debug.LogError($"Duplicate scene entity ID: {entity.EntityId}", entity);
                    else entities.Add(entity.EntityId, entity);
                }
            }
        }

        public bool TryGet(string entityId, out SceneEntity entity) =>
            entities.TryGetValue(entityId, out entity);
    }
}
