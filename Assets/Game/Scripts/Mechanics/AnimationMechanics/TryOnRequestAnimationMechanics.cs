using Atomic.Elements;
using UnityEngine;

public class TryOnRequestAnimationMechanics
{
    private readonly Animator _animator;
    private readonly AnimationDispatcher _animationDispatcher;
    private readonly IAtomicObservable _actionRequest;
    private readonly IAtomicAction _tryAction;
    private readonly IAtomicValue<bool> _actionCondition;
    private readonly string _animationDispatcherKey;
    private readonly int _animatorTriggerHash;

    public TryOnRequestAnimationMechanics(
        Animator animator,
        AnimationDispatcher animationDispatcher,
        IAtomicObservable actionRequest,
        IAtomicAction tryAction,
        IAtomicValue<bool> actionCondition,
        string animationDispatcherKey,
        int animatorTriggerHash
    )
    {
        _animator = animator;
        _animationDispatcher = animationDispatcher;
        _actionRequest = actionRequest;
        _tryAction = tryAction;
        _actionCondition = actionCondition;
        _animationDispatcherKey = animationDispatcherKey;
        _animatorTriggerHash = animatorTriggerHash;
    }

    public void OnEnable()
    {
        _actionRequest.Subscribe(OnActionRequest);
        _animationDispatcher.SubscribeOnEvent(_animationDispatcherKey, _tryAction.Invoke);
    }

    public void OnDisable()
    {
        _actionRequest.Unsubscribe(OnActionRequest);
        _animationDispatcher.UnsubscribeOnEvent(_animationDispatcherKey, _tryAction.Invoke);
    }

    private void OnActionRequest()
    {
        if (_actionCondition.Value)
        {
            _animator.SetTrigger(_animatorTriggerHash);
        }
    }
}