using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemyAttackAgent : UnitComponent, IInitializable, IGameFixedUpdateListener
    {
        private float _attackTime;
        private EnemyMoveAgent _moveAgent;
        private GameObject _target;
        private HitPointsComponent _targetHitPoints;
        private Timer _attackTimer;
        private BulletManager _bulletManager;

        [Inject]
        private void Construct(
            BulletManager bulletManager,
            float attackTime
            )
        {
            _bulletManager = bulletManager;
            _attackTime = attackTime;
        }

        public void Initialize()
        {
            IGameListener.Register(this);
            _attackTimer = new Timer(_attackTime);
            _moveAgent = ((Enemy)_componentOwner).EnemyMoveAgent;
        }

        public void OnGameFixedUpdate(float fixedDeltaTime)
        {
            if (!Condition.IsTrue())
            {
                return;
            }

            if (_attackTimer.Tick(fixedDeltaTime))
            {
                Attack();
            }
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
            _targetHitPoints = target.GetComponent<Unit>().HitPointsComponent;
        }

        private void Attack()
        {
            var startPosition = _componentOwner.WeaponComponent.Position;
            var vector = (Vector2) _target.transform.position - startPosition;
            var direction = vector.normalized;
            _bulletManager.Shoot(_componentOwner, direction);
        }
    }
}