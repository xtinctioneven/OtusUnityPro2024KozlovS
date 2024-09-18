using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class ProjectileAimSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<MoveDirection, Rotation, ProjectileTag>, Exc<Inactive>> _filter;
        
        public void Run (IEcsSystems systems) {
            float deltaTime = UnityEngine.Time.deltaTime;
            
            EcsPool<MoveDirection> directionPool = _filter.Pools.Inc1;
            EcsPool<Rotation> rotationPool = _filter.Pools.Inc2;
            
            foreach (int entity in _filter.Value)
            {
                Vector3 rotationDir = rotationPool.Get(entity).Value * Vector3.forward;
                ref MoveDirection moveDirection = ref directionPool.Get(entity);
                moveDirection.Value = rotationDir;
            }
        }
    }
}