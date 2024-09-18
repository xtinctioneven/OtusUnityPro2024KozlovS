using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Zenject;

namespace Client
{
    public class GameOverEventSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<GameOverEvent>> _gameOverFilter = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<Position>, Exc<Inactive>> _activeEntitiesFilter;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        // [Inject]
        // public GameOverEventSystem(EntityManager entityManager)
        // {
        //     _entityManager.Value = entityManager;
        // }
        
        public void Run(IEcsSystems systems)
        {
            if (_gameOverFilter.Value.GetEntitiesCount() <= 0)
            {
                return;
            }
            foreach (int @event in _gameOverFilter.Value)
            {
                _eventWorld.Value.DelEntity(@event);
            }
            foreach (var entity in _activeEntitiesFilter.Value)
            { 
                _entityManager.Value.Get(entity).AddData(new Inactive());
            }
        }
    }
}