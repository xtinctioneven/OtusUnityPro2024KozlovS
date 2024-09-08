using System;
using Atomic.Elements;
using UnityEngine;

[Serializable]
public class RotateComponent
{
    public Transform RotationRoot => _rotationRoot;
    public AtomicAction<Vector3> RotateAction;
    [SerializeField] private Transform _rotationRoot;
    [SerializeField] private Vector3 _rotationDirection;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private bool _canRotate = true;
    
    private readonly CompositeCondition _compositeCondition = new();
    
    public void Compose()
    {
        RotateAction.Compose(Rotate);
    }

    public void Rotate(Vector3 rotationDirection)
    {
        _rotationDirection = rotationDirection;
        if (!_canRotate || !_compositeCondition.IsTrue())
        {
            return;
        }

        if (rotationDirection == Vector3.zero)
        {
            return;
        }
        
        var targetRotation = Quaternion.LookRotation(_rotationDirection, Vector3.up);
        _rotationRoot.rotation = Quaternion.Lerp(_rotationRoot.rotation, targetRotation, _rotationSpeed);
    }

    public void AppendCondition(Func<bool> condition)
    {
        _compositeCondition.Append(condition);
    }
}