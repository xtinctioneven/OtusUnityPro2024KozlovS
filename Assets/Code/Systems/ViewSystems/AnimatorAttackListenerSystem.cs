using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class AnimatorAttackListenerSystem : IEcsRunSystem
    {
        private static readonly int _attackAnimatorTrigger = Animator.StringToHash("Attack");
        private readonly EcsFilterInject<Inc<AttackAnimationRequest, AnimatorView>> _filter;

        public void Run(IEcsSystems systems)
        {
            EcsPool<AttackAnimationRequest> _requestPool = _filter.Pools.Inc1;
            foreach (var request in _filter.Value)
            {
                Animator animator = _filter.Pools.Inc2.Get(request).Value;
                animator.SetTrigger(_attackAnimatorTrigger);
                _requestPool.Del(request);
            }
        }
    }
}