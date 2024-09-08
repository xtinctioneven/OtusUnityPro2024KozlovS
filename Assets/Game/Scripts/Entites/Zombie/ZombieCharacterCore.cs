using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

[Serializable]
public  class ZombieCharacterCore
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _target;
    public LifeComponent LifeComponent;
    [SerializeField] private float _offsetDistance;
    public MoveComponent MoveComponent;
    public RotateComponent RotateComponent;
    public StrikeComponent StrikeComponent;
    
    private LookAtTargetMechanics _lookAtTargetMechanics;
    private FollowTargetMechanics _followTargetMechanics;
    private AutoAttackTargetMechanics _autoAttackTargetMechanics;
    private DamageTargetOnEventMechanics _dealDamageOnEventMechanics;

    public void Compose(Transform target)
    {
        _target = target;
        LifeComponent.Compose();
        MoveComponent.Compose();
        MoveComponent.AppendCondition(LifeComponent.IsAlive);
        RotateComponent.Compose();
        RotateComponent.AppendCondition(LifeComponent.IsAlive);
        StrikeComponent.Compose();
        StrikeComponent.AppendCondtion(LifeComponent.IsAlive);
        StrikeComponent.AppendCondtion(() => !MoveComponent.IsMoving.Value);
        
        _autoAttackTargetMechanics = new AutoAttackTargetMechanics(StrikeComponent.StrikeRequest, StrikeComponent.CanStrike);

        var targetPosition = new AtomicFunction<Vector3>(() =>
        {
            return _target.position;
        });
        var selfPosition = new AtomicFunction<Vector3>(() =>
        {
            return MoveComponent.Root.position;
        });
        _lookAtTargetMechanics =
            new LookAtTargetMechanics(RotateComponent.RotateAction, targetPosition, selfPosition);
        
        var updateMoveDirection = new AtomicAction<Vector3>(moveDir =>
        {
            MoveComponent.MoveDirection.Value = moveDir;
        });
        var followOffset = new AtomicFunction<Vector3>(() =>
        {
            Vector3 followOffset = (selfPosition.Value - _target.position).normalized;
            followOffset = followOffset * _offsetDistance;
            return followOffset;
        });
        
        _followTargetMechanics = new FollowTargetMechanics(updateMoveDirection, targetPosition, selfPosition, followOffset);
        
        _target.TryGetComponent(out IAtomicEntity targetEntity); 
        _dealDamageOnEventMechanics = new DamageTargetOnEventMechanics(StrikeComponent.StrikeConnectedEvent, targetEntity);
    }

    public void Update(float deltaTime)
    {
        MoveComponent.Update(deltaTime);
        StrikeComponent.Update(deltaTime);
        
        _lookAtTargetMechanics.Update();
        _followTargetMechanics.Update();
        _autoAttackTargetMechanics.Update();
    }
}