using Atomic.Elements;
using UnityEngine;

public class BoolAnimationMechanics
{
    private readonly IAtomicObservable<bool> _isActive;
    private readonly Animator _animator;
    private readonly int _animatorKey;

    public BoolAnimationMechanics(IAtomicObservable<bool> isActive, Animator animator, int animatorKey)
    {
        _isActive = isActive;
        _animator = animator;
        _animatorKey = animatorKey;
    }

    public void OnEnable()
    {
        _isActive.Subscribe(OnValueChanged);
    }

    public void OnDisable()
    {
        _isActive.Unsubscribe(OnValueChanged);
        OnValueChanged(false);
    }

    private void OnValueChanged(bool isActive)
    {
        _animator.SetBool(_animatorKey, isActive);
    }
}