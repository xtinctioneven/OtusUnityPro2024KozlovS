using System;
using UnityEngine;

[Serializable]
public class PlayerCharacterAnimation
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationDispatcher _animationDispatcher;

    private PlayerCharacterCore _playerCharacterCore;
    private BoolAnimationMechanics _moveAnimationMechanics;
    private BoolAnimationMechanics _deathAnimationMechanics;
    private TryOnRequestAnimationMechanics _shootAnimationMechanics;
    private TriggerAnimationMechanics _takeDamageAnimationMechanics;

    private readonly int _isMoving = Animator.StringToHash("IsMoving");
    private readonly int _isDead = Animator.StringToHash("IsDead");
    private readonly string _shootDispatcherKey = "ShootAnimationEvent";
    private readonly int _shootTrigger = Animator.StringToHash("Shoot");
    private readonly int _takeDamageTrigger = Animator.StringToHash("TakeDamage");
    public void Compose(PlayerCharacterCore playerCharacterCore)
    {
        _playerCharacterCore = playerCharacterCore;
        
        _moveAnimationMechanics = new BoolAnimationMechanics(_playerCharacterCore.MoveComponent.IsMoving,
            _animator, _isMoving);
        _deathAnimationMechanics = new BoolAnimationMechanics(_playerCharacterCore.LifeComponent.IsDead,
            _animator, _isDead);
        _shootAnimationMechanics = new TryOnRequestAnimationMechanics(
            _animator, _animationDispatcher, _playerCharacterCore.ShootComponent.ShootRequest,
            _playerCharacterCore.ShootComponent.ShootTryAction, _playerCharacterCore.ShootComponent.CanFire,
            _shootDispatcherKey, _shootTrigger);
        _takeDamageAnimationMechanics = new TriggerAnimationMechanics(_animator,
            _playerCharacterCore.LifeComponent.OnDamageTakenEvent, _takeDamageTrigger);
    }

    public void OnEnable()
    {
        _shootAnimationMechanics.OnEnable();
        _moveAnimationMechanics.OnEnable();
        _deathAnimationMechanics.OnEnable();
        _takeDamageAnimationMechanics.OnEnable();
    }

    public void OnDisable()
    {
        _shootAnimationMechanics.OnDisable();
        _moveAnimationMechanics.OnDisable();
        _deathAnimationMechanics.OnDisable();
        _takeDamageAnimationMechanics.OnDisable();
    }
}