using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    public class ProjectileAttackRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackRequest, ProjectileWeapon, TeamId, AttackTargetEntity>, Exc<Inactive>> _filter;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<SpawnRequest> _spawnRequestPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Prefab> _prefabPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<TeamId> _teamIdPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<AttackTargetEntity> _attackTargetPool = EcsWorlds.EVENTS;

        public void Run(IEcsSystems systems)
        {
            EcsPool<ProjectileWeapon> weaponPool = _filter.Pools.Inc2;
            EcsPool<AttackRequest> requestPool= _filter.Pools.Inc1;
            EcsPool<TeamId> teamIdPool = _filter.Pools.Inc3;
            EcsPool<AttackTargetEntity> targetPool = _filter.Pools.Inc4;

            foreach (var entity in _filter.Value)
            {
                ProjectileWeapon weapon = weaponPool.Get(entity);
                int spawnEvent = _eventWorld.Value.NewEntity();
                
                _spawnRequestPool.Value.Add(spawnEvent) = new SpawnRequest();
                _positionPool.Value.Add(spawnEvent) = new Position {Value = weapon.FirePoint.position};
                _rotationPool.Value.Add(spawnEvent) = new Rotation {Value = weapon.FirePoint.rotation};
                _prefabPool.Value.Add(spawnEvent) = new Prefab {Value = weapon.ProjectilePrefab};
                _teamIdPool.Value.Add(spawnEvent) = teamIdPool.Get(entity);
                _attackTargetPool.Value.Add(spawnEvent) = targetPool.Get(entity);
                requestPool.Del(entity);
            }
            
        }
    }
}