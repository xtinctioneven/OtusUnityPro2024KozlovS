using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client
{
    public class ProjectileDeathEventSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ProjectileTag, DeathEvent, Inactive>> _filter;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _entityManager.Value.Get(entity).AddData(new DestroyRequest());
            }
        }
    }
}