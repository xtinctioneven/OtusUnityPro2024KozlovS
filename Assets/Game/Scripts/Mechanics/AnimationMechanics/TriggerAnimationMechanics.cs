using Atomic.Elements;
using UnityEngine;

public class TriggerAnimationMechanics
{
    private Animator _animator;
    private readonly IAtomicObservable _actionTrigger;
    private readonly int _animatorTriggerHash;

    public TriggerAnimationMechanics(
        Animator animator,
        IAtomicObservable actionTrigger,
        int animatorTriggerHash
    )
    {
        _animator = animator;
        _actionTrigger = actionTrigger;
        _animatorTriggerHash = animatorTriggerHash;
    }

    public void OnEnable()
    {
        _actionTrigger.Subscribe(OnTrigger);
    }

    public void OnDisable()
    {
        _actionTrigger.Unsubscribe(OnTrigger);
    }

    private void OnTrigger()
    {
        _animator.SetTrigger(_animatorTriggerHash);
    }
}