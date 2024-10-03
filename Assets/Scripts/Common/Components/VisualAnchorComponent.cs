using UnityEngine;

public sealed class VisualAnchorComponent
{
    public Transform Value { get; }
    public Vector3 Position => Value.transform.position;

    public VisualAnchorComponent(Transform transform)
    {
        Value = transform;
    }
}