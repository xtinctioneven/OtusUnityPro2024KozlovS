using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;

namespace Client
{
    public class ProjectileCollisionRequestSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionEnterRequest, ProjectileTag, SourceEntity, DamageTargetEntity>> _filter 
            = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsFactoryInject<TakeDamageRequest, SourceEntity, DamageTargetEntity, Damage> _takeDamageEmitter 
            = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<Damage> _damagePool;
        private readonly EcsPoolInject<DeathRequest> _deathRequestPool;
        private readonly EcsPoolInject<DamageableTag> _damageableTagPool;

        public void Run(IEcsSystems systems)
        {
            EcsPool<SourceEntity> sourcePool = _filter.Pools.Inc3;
            EcsPool<DamageTargetEntity> targetPool = _filter.Pools.Inc4;
            
            foreach (var entity in _filter.Value)
            {
                SourceEntity sourceEntity = sourcePool.Get(entity);
                int projectile = sourceEntity.Value;
                if (!_deathRequestPool.Value.Has(projectile))
                {
                    DamageTargetEntity targetEntity = targetPool.Get(entity);
                    int target = targetEntity.Value;
                    if (_damageableTagPool.Value.Has(target))
                    {
                        _takeDamageEmitter.Value.NewEntity(
                            new TakeDamageRequest(),
                            sourceEntity,
                            targetEntity,
                            _damagePool.Value.Get(projectile)
                        );
                    }
                    _deathRequestPool.Value.Add(projectile);
                }
                
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}