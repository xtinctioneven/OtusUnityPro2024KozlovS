using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class AnimatorMoveListenerSystem : IEcsRunSystem
    {
        private static readonly int _isMovingAnimatorBool = Animator.StringToHash("IsMoving");
        private readonly EcsFilterInject<Inc<MoveDirection, AnimatorView>> _filter;

        public void Run(IEcsSystems systems)
        {
            EcsPool<MoveDirection> _moveDirectionPool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                Animator animator = _filter.Pools.Inc2.Get(entity).Value;
                bool isMoving = _moveDirectionPool.Get(entity).Value != Vector3.zero;
                animator.SetBool(_isMovingAnimatorBool, isMoving);
            }
        }
    }
}