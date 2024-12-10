using System;
using UnityEngine;

[Serializable]
public struct VfxData
{
    public GameObject Prefab;
    public float CollisionDelay;
    public float JumpPower;
    public VfxAppearanceType AppearanceType;
}