using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    public sealed class ClearTargetOnDeathEventSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<DeathEvent>> _deadEntitiesFilter;
        private readonly EcsFilterInject<Inc<AttackTargetEntity>, Exc <Inactive>> _aliveEntitiesFilter;
        private readonly EcsPoolInject<Inactive> _inactivePool;
        private readonly EcsPoolInject<DeathEvent> _eventPool;
        
        public void Run (IEcsSystems systems)
        {
            EcsPool<AttackTargetEntity> attackTargetsPool = _aliveEntitiesFilter.Pools.Inc1;
            EcsPool<CanAttack> canAttackPool = systems.GetWorld().GetPool<CanAttack>();
            EcsPool<TargetRequest> targetRequestPool = systems.GetWorld().GetPool<TargetRequest>();
            foreach (int deadEntity in _deadEntitiesFilter.Value)
            {
                foreach (int aliveEntity in _aliveEntitiesFilter.Value)
                {
                    if (attackTargetsPool.Get(aliveEntity).Value == deadEntity)
                    {
                        attackTargetsPool.Del(aliveEntity);
                        targetRequestPool.Add(aliveEntity);
                        canAttackPool.Del(aliveEntity);
                    }
                }
            }
        }
    }
}