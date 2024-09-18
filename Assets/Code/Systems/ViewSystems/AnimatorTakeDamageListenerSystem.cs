using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client
{
    public class AnimatorTakeDamageListenerSystem : IEcsRunSystem
    {
        private static readonly int TakeDamageAnimatorTrigger = Animator.StringToHash("TakeDamage");
        private readonly EcsFilterInject<Inc<TakeDamageEvent, DamageTargetEntity>> _filter = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<AnimatorView> _animatorPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                int target = _filter.Pools.Inc2.Get(@event).Value;
                
                if (_animatorPool.Value.Has(target))
                {
                    Animator animator = _animatorPool.Value.Get(target).Value;
                    animator.SetTrigger(TakeDamageAnimatorTrigger);
                }
            }
        }
    }
}