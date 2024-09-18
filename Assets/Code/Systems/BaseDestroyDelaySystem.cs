using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client
{
    public class BaseDestroyDelaySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Timer, BaseTag, Inactive>> _filter;
        private readonly EcsCustomInject<EntityManager> _entityManager;
        public void Run(IEcsSystems systems)
        {
            if (_filter.Value.GetEntitiesCount() <= 0)
            {
                return;
            }
            float deltaTime = UnityEngine.Time.deltaTime;
            EcsPool<Timer> timerPool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                ref Timer timer = ref timerPool.Get(entity);
                timer.CurrentValue -= deltaTime;
                if (timer.CurrentValue <= 0)
                {
                    _entityManager.Value.Get(entity).AddData(new DestroyRequest());
                    _entityManager.Value.Get(entity).RemoveData<Timer>();
                }
            }
        }
    }
}