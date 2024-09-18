using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Zenject;

namespace Client
{
    public class SpawnRequestSystem: IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, Prefab, TeamId, AttackTargetEntity>> _filter = EcsWorlds.EVENTS;

        private readonly EcsCustomInject<EntityManager> _entityManager;
        private DiContainer _diContainer;

        public SpawnRequestSystem(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int @event in _filter.Value)
            {
                Vector3 position = _filter.Pools.Inc2.Get(@event).Value;
                Quaternion rotation = _filter.Pools.Inc3.Get(@event).Value;
                Entity prefab = _filter.Pools.Inc4.Get(@event).Value;
                Team teamId = _filter.Pools.Inc5.Get(@event).Value;
                int attackTargetEntity = _filter.Pools.Inc6.Get(@event).Value;
                Entity entity = _diContainer.InstantiatePrefab(prefab, position, rotation, null).GetComponent<Entity>();
                entity.Initialize(systems.GetWorld());
                entity.AddData(new TeamId {Value = teamId});
                entity.AddData(new AttackTargetEntity {Value = attackTargetEntity});
                _entityManager.Value.Add(entity);
                _eventWorld.Value.DelEntity(@event);
            }
        }
    }
}