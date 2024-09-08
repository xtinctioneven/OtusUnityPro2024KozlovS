using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public class CameraEntity : AtomicEntity
{
    public MoveComponent MoveComponent;
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Vector3 _followOffset;

    private FollowTargetMechanics _followTargetMechanics;

    public void Awake()
    {
        MoveComponent.Compose();

        var updateMoveDirection = new AtomicAction<Vector3>(moveDir =>
        {
            MoveComponent.MoveDirection.Value = moveDir;
        });
        var followTarget = new AtomicFunction<Vector3>(() =>
        {
            return _followTarget.position;
        });
        var selfTransform = new AtomicFunction<Vector3>(() =>
        {
            return this.transform.position;
        });
        var followOffset = new AtomicFunction<Vector3>(() =>
        {
            return _followOffset;
        });
        
        _followTargetMechanics = new FollowTargetMechanics(updateMoveDirection, followTarget,
            selfTransform, followOffset);
    }

    public void Update()
    {
        MoveComponent.Update(Time.deltaTime);
    
        _followTargetMechanics.Update();
    }
}