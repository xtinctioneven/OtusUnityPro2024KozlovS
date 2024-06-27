using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        public bool IsReached
        {
            get { return _isReached; }
        }

        [SerializeField] private MoveComponent _moveComponent;

        private Vector2 _destination;
        private bool _isReached;

        public void Start()
        {
            IGameListener.Register(this);
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        public void OnGameFixedUpdate(float fixedDeltaTime)
        {
            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= 0.25f)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * fixedDeltaTime;
            _moveComponent.MoveInDirection(direction);
        }
    }
}