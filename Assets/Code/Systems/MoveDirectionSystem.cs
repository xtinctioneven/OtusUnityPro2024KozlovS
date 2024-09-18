using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class MoveDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, AttackDistance, AttackTargetEntity, Position>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<CanAttack> _canAttackPool;
        
        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            EcsPool<MoveDirection> moveDirectionPool = _filter.Pools.Inc1;
            EcsPool<AttackDistance> attackDistancePool = _filter.Pools.Inc2;
            EcsPool<AttackTargetEntity> targetsPool = _filter.Pools.Inc3;
            EcsPool<Position> positionPool = _filter.Pools.Inc4;
            foreach (var entity in _filter.Value)
            {
                int targetEntityId = targetsPool.Get(entity).Value;
                Vector3 targetDirection = positionPool.Get(targetEntityId).Value - positionPool.Get(entity).Value;
                float attackDistance = attackDistancePool.Get(entity).Value;
                if (targetDirection.magnitude > attackDistance)
                {
                    if (_canAttackPool.Value.Has(entity))
                    {
                        _canAttackPool.Value.Del(entity);
                    }
                    ref MoveDirection moveDirection = ref moveDirectionPool.Get(entity);
                    moveDirection.Value = targetDirection.normalized;
                }
                else
                {
                    if (!_canAttackPool.Value.Has(entity))
                    {
                        _canAttackPool.Value.Add(entity);
                    }
                    moveDirectionPool.Get(entity).Value = Vector3.zero;
                }
            }
        }
    }
}