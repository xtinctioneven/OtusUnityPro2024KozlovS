using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemyMoveAgent : UnitComponent, IGameFixedUpdateListener, IInitializable
    {
        private MoveComponent _moveComponent;
        private Vector2 _destination;
        private bool _isReached;
        private Transform _thisTransform;

        [Inject]
        private void Construct(MoveComponent moveComponent)
        {
            _moveComponent = moveComponent;
        }

        public void Initialize()
        {
            IGameListener.Register(this);
            _thisTransform = _componentOwner.gameObject.transform;
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

            var vector = _destination - (Vector2)_thisTransform.position;
            if (vector.magnitude <= 0.25f)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * fixedDeltaTime;
            _moveComponent.MoveInDirection(direction);
        }

        public bool IsReached()
        {
            return _isReached; 
        }
    }
}