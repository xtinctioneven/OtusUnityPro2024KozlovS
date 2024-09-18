using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Unity.VisualScripting;

namespace Client
{
    public sealed class DeathRequestSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<DeathRequest>, Exc <Inactive>> _filter;
        private readonly EcsPoolInject<Inactive> _inactivePool;
        private readonly EcsPoolInject<DeathEvent> _eventPool;
        
        public void Run (IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);
                _inactivePool.Value.Add(entity) = new Inactive();
                _eventPool.Value.Add(entity) = new DeathEvent();
            }
        }
    }
}