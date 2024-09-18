using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class MovementSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<MoveDirection, Position, MoveSpeed>, Exc<Inactive>> _filter;
        
        public void Run (IEcsSystems systems) {
            float deltaTime = UnityEngine.Time.deltaTime;
            
            EcsPool<MoveDirection> directionPool = _filter.Pools.Inc1;
            EcsPool<Position> positionPool = _filter.Pools.Inc2;
            EcsPool<MoveSpeed> speedPool = _filter.Pools.Inc3;
            
            foreach (int entity in _filter.Value)
            {
                MoveDirection moveDirection = directionPool.Get(entity);
                MoveSpeed moveSpeed = speedPool.Get(entity);
                ref Position position = ref positionPool.Get(entity);
                position.Value += moveDirection.Value * (moveSpeed.Value * deltaTime);
            }
        }
    }
}