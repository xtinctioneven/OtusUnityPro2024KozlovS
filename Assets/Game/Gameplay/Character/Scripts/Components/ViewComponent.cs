using System;
using Game.Gameplay;
using UnityEngine;

[Serializable]
public class ViewComponent
{
    public EntityView Value { get; }
    public Vector3 Position => Value.transform.position;

    public ViewComponent(EntityView entityView)
    {
        Value = entityView;
    }

    public void SetPosition(Vector3 position)
    {
        Value.transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        Value.transform.rotation = rotation;
    }
}