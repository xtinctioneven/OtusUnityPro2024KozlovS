using UnityEngine;

namespace Game.Gameplay
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private Transform _visualRoot;
        private AnimationDispatcher _animationDispatcher;
        private Animator _animator;
        private GameObject _entityVisual;

        // private void Awake()
        // {
        //     _animationDispatcher.SubscribeOnEvent("AnimationEnd", Test);
        // }

        public void Setup(GameObject entityVisualPrefab)
        {
            if (entityVisualPrefab == null)
            {
                Debug.LogError("No visual, please assign visualPrefab");
            }
            _entityVisual = GameObject.Instantiate(entityVisualPrefab, _visualRoot);
            _animator = _entityVisual.GetComponent<Animator>();
            _animationDispatcher = _entityVisual.GetComponent<AnimationDispatcher>();
            _animationDispatcher.SubscribeOnEvent("AnimationEnd", Test);
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        public void Test()
        {
            Debug.Log("Test");
        }
    }
}