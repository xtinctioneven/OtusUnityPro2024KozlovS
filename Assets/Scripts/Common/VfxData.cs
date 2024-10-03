using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct VfxData
{
    public ParticleSystem Vfx;
    public bool IsAttachedToComponent;
    public int Duration;
    public VfxTargetType TargetType;
    
    public enum VfxTargetType
    {
        AtSelf = 0,
        AtSelfRotatedToTarget = 10,
        AtSelfMovingToTarget = 15,
        AtTarget = 20
    }
}