using UnityEngine;

public sealed class TransformComponent
{
    public Transform Value { get; }
    public Vector3 Position => Value.transform.position;

    public TransformComponent(Transform transform)
    {
        Value = transform;
    }
}