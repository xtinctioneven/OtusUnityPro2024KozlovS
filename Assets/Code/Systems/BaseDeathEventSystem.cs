using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client
{
    public class BaseDeathEventSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BaseTag, DeathEvent, Inactive>> _filter;
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<GameOverEvent> _gameOverPool = EcsWorlds.EVENTS;
        private readonly EcsCustomInject<EntityManager> _entityManager;
        public void Run(IEcsSystems systems)
        {
            if (_filter.Value.GetEntitiesCount() <= 0)
            {
                return;
            }
            foreach (var entity in _filter.Value)
            {
                _entityManager.Value.Get(entity).AddData(new Timer{CurrentValue = 1.5f});
            }

            var @event = _eventWorld.Value.NewEntity();
            _gameOverPool.Value.Add(@event) = new GameOverEvent();
        }
    }
}