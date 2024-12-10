using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private Transform _visualRoot;
        [field: SerializeField] public HealthBarView HealthView {get; private set; }
        public AnimationDispatcher AnimationDispatcher {get; private set;}
        public Transform CastTarget {get; private set;}
        public Transform CastSource {get; private set;}
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
            
            VfxTarget vfxTarget = _entityVisual.gameObject.GetComponentInChildren<VfxTarget>();
            VfxSource vfxSource = _entityVisual.gameObject.GetComponentInChildren<VfxSource>();
            CastTarget = vfxTarget != null ? vfxTarget.transform : _entityVisual.transform;
            CastSource = vfxSource != null ? vfxSource.transform : _entityVisual.transform;
            if (vfxTarget != null)
            {
                Destroy(vfxTarget);
            }
            if (vfxSource != null)
            {
                Destroy(vfxSource);
            }
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }
    }
}