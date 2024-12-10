using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private Transform _visualRoot;
        public AnimationDispatcher AnimationDispatcher {get; private set;}
        public Animator Animator {get; private set;}
        private GameObject _entityVisual;

        public void Setup(GameObject entityVisualPrefab)
        {
            if (entityVisualPrefab == null)
            {
                Debug.LogError("No visual, please assign visualPrefab");
            }
            _entityVisual = GameObject.Instantiate(entityVisualPrefab, _visualRoot);
            Animator = _entityVisual.GetComponent<Animator>();
            AnimationDispatcher = _entityVisual.GetComponent<AnimationDispatcher>();
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }
    }
}