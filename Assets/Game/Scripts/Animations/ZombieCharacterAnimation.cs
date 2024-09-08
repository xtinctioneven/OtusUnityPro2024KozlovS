using System;
using UnityEngine;

[Serializable]
public class ZombieCharacterAnimation 
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationDispatcher _animationDispatcher;

    private ZombieCharacterCore _zombieCharacterCore;
    private BoolAnimationMechanics _moveAnimationMechanics;
    private TryOnRequestAnimationMechanics _strikeAnimationMechanics;
    
    private readonly int _isMoving = Animator.StringToHash("IsMoving");
    private readonly string _strikeDispatcherKey = "StrikeAnimationEvent";
    private readonly int _strikeTrigger = Animator.StringToHash("Strike");

    public void Compose(ZombieCharacterCore zombieCharacterCore)
    {
        _zombieCharacterCore = zombieCharacterCore;
        
        _moveAnimationMechanics = new BoolAnimationMechanics(_zombieCharacterCore.MoveComponent.IsMoving,
            _animator, _isMoving);
        _strikeAnimationMechanics = new TryOnRequestAnimationMechanics(
            _animator, _animationDispatcher, _zombieCharacterCore.StrikeComponent.StrikeRequest,
            _zombieCharacterCore.StrikeComponent.StrikeStartAction, _zombieCharacterCore.StrikeComponent.CanStrike,
            _strikeDispatcherKey, _strikeTrigger);
    }

    public void OnEnable()
    {
        _strikeAnimationMechanics.OnEnable();
        _moveAnimationMechanics.OnEnable();
    }

    public void OnDisable()
    {
        _strikeAnimationMechanics.OnDisable();
        _moveAnimationMechanics.OnDisable();
    }
}