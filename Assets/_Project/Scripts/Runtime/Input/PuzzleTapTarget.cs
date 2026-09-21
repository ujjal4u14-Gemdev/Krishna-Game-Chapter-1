using UnityEngine;
using UnityEngine.EventSystems;

namespace MythicPuzzle.Runtime
{
    [RequireComponent(typeof(SceneEntity))]
    public sealed class PuzzleTapTarget : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private PuzzleInputRouter inputRouter;
        private SceneEntity sceneEntity;

        private void Awake() => sceneEntity = GetComponent<SceneEntity>();

        public void OnPointerClick(PointerEventData eventData)
        {
            inputRouter.RaiseTap(sceneEntity.EntityId, eventData.position);
        }
    }
}

