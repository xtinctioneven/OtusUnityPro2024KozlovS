using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class AttackOnTimerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CanAttack,Timer>, Exc<Inactive, TargetRequest>> _filter;
        private readonly EcsPoolInject<AttackAnimationRequest> _attackRequestPool;
        
        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            EcsPool<Timer> timerPool = _filter.Pools.Inc2;
            foreach (var entity in _filter.Value)
            {
                ref Timer timer = ref timerPool.Get(entity);
                timer.CurrentValue -= deltaTime;
                if (timer.CurrentValue <= 0)
                {
                    timer.CurrentValue += timer.TimerValue;
                    _attackRequestPool.Value.Add(entity) = new AttackAnimationRequest();
                }
            }
        }
    }
}