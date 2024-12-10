using System;
using Game.Gameplay;
using UnityEngine;

[Serializable]
public class VfxAnchorComponent
{
    public readonly Transform CastTarget;
    public readonly Transform CastSource;

    public VfxAnchorComponent(EntityView entityView)
    {
        CastSource = entityView.CastSource;
        CastTarget = entityView.CastTarget;
    }
}