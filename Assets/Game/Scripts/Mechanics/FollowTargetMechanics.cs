using Atomic.Elements;
using UnityEngine;

public class FollowTargetMechanics
{
    private readonly IAtomicAction<Vector3> _followAction;
    private readonly IAtomicValue<Vector3> _followPosition;
    private readonly IAtomicValue<Vector3> _selfPosition;
    private readonly IAtomicValue<Vector3> _followOffset;

    public FollowTargetMechanics(
        IAtomicAction<Vector3> followAction,
        IAtomicValue<Vector3> followPosition,
        IAtomicValue<Vector3> selfPosition,
        IAtomicValue<Vector3> followOffset = default
    )
    {
        _followAction = followAction;
        _followPosition = followPosition;
        _selfPosition = selfPosition;
        _followOffset = followOffset;
    }

    public void Update()
    {
        Vector3 moveDirection = (_followPosition.Value - _selfPosition.Value + _followOffset.Value);
        if (moveDirection.magnitude < .05f)
        {
            _followAction.Invoke(Vector3.zero);
        }
        else
        {
            _followAction.Invoke(moveDirection.normalized);
        }
    }
}