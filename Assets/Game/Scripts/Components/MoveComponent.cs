using System;
using Atomic.Elements;
using UnityEngine;

[Serializable]
public class MoveComponent
{
    public AtomicVariable<Vector3> MoveDirection;
    public AtomicVariable<bool> IsMoving;
    public Transform Root;

    [SerializeField] private float _speed = 2f;
    [SerializeField] private bool _canMove = true;

    private readonly CompositeCondition _compositeCondition = new();

    public void Compose()
    {
        MoveDirection.Subscribe(moveDirection =>
        {
            IsMoving.Value = moveDirection != Vector3.zero;
        });
    }

    public void Update(float deltaTime)
    {
        if (_compositeCondition.IsTrue() && _canMove)
        {
            Root.position += MoveDirection.Value * _speed * deltaTime;
        }
    }

    public void AppendCondition(Func<bool> condition)
    {
        _compositeCondition.Append(condition);
    }
}