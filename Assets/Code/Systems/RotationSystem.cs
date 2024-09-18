using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class RotationSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<Rotation, RotationSpeed, Position, AttackTargetEntity>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<Position> _allPositionPool;
        
        public void Run (IEcsSystems systems) {
            float deltaTime = UnityEngine.Time.deltaTime;
            
            EcsPool<Rotation> rotationPool = _filter.Pools.Inc1;
            EcsPool<RotationSpeed> rotationSpeedPool = _filter.Pools.Inc2;
            EcsPool<Position> positionPool = _filter.Pools.Inc3;
            EcsPool<AttackTargetEntity> targetPool = _filter.Pools.Inc4;
            
            foreach (int entity in _filter.Value)
            {
                Vector3 position = positionPool.Get(entity).Value;
                int targetEntity = targetPool.Get(entity).Value;
                Vector3 targetPosition = _allPositionPool.Value.Get(targetEntity).Value;
                Vector3 direction = targetPosition - position;
                direction.y = 0;
                ref Rotation rotation = ref rotationPool.Get(entity);
                float rotationSpeed = rotationSpeedPool.Get(entity).Value;
                Quaternion endRotation = Quaternion.LookRotation(direction, Vector3.up);
                rotation.Value = Quaternion.Slerp(rotation.Value, endRotation, deltaTime * rotationSpeed);
            }
        }
    }
}