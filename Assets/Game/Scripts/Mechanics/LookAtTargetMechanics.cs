using Atomic.Elements;
using UnityEngine;

public class LookAtTargetMechanics
{
    private readonly IAtomicAction<Vector3> _rotateAction;
    private readonly IAtomicValue<Vector3> _targetPoint;
    private readonly IAtomicValue<Vector3> _selfTransform;

    public LookAtTargetMechanics(
        IAtomicAction<Vector3> rotateAction,
        IAtomicValue<Vector3> targetPoint,
        IAtomicValue<Vector3> selfTransform
    )
    {
        _rotateAction = rotateAction;
        _targetPoint = targetPoint;
        _selfTransform = selfTransform;
    }

    public void Update()
    {
        Vector3 direction = _targetPoint.Value - _selfTransform.Value;
        _rotateAction.Invoke(direction);
    }
}