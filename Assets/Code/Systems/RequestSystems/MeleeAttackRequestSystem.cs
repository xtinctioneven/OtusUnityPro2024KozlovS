using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;

namespace Client
{
    public class MeleeAttackRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackRequest, Damage, AttackTargetEntity>, Exc<Inactive, ProjectileWeapon>> _filter;
        private readonly EcsFactoryInject<TakeDamageRequest, SourceEntity, DamageTargetEntity, Damage> _takeDamageEmitter 
            = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<DeathRequest> _deathRequestPool;
        private readonly EcsPoolInject<DamageableTag> _damageableTagPool;

        public void Run(IEcsSystems systems)
        {
            EcsPool<AttackRequest> requestPool = _filter.Pools.Inc1;
            EcsPool<Damage> damagePool = _filter.Pools.Inc2;
            EcsPool<AttackTargetEntity> targetPool = _filter.Pools.Inc3;
            EcsWorld ecsWorld = systems.GetWorld();
            foreach (var entity in _filter.Value)
            {
                SourceEntity sourceEntity = new SourceEntity { Value = entity };
                if (!_deathRequestPool.Value.Has(entity))
                {
                    DamageTargetEntity targetEntity = new DamageTargetEntity { Value = targetPool.Get(entity).Value };
                    int target = targetEntity.Value;
                    if (_damageableTagPool.Value.Has(target))
                    {
                        _takeDamageEmitter.Value.NewEntity(
                            new TakeDamageRequest(),
                            sourceEntity,
                            targetEntity,
                            damagePool.Get(entity)
                        );
                    }
                }
                requestPool.Del(entity);
            }
        }
    }
}