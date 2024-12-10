using System;
using UnityEngine.Serialization;

[Serializable]
public class AbilityVisualData
{
    // public GameObject _casterFx;
    // public bool _parentCasterFx;
    // public GameObject _targetFx;
    // public string _targetTargetTransform;
    public VfxData AbilityVfxData;
    public AbilityCastType CastType = AbilityCastType.Melee;
    public AbilityCollisionType CollisionType = AbilityCollisionType.None;
    public AnimationClipType SourceAnimationClip = AnimationClipType.AttackStrike1;  
    public float MeelePositionOffset = 1f;
    // public AnimationClipType TargetAnimationClip = AnimationClipType.Hit;
    // public string _impactAnimation = "Impact";
    // public float _casterTargetFxSpeed = 1;
    // public float _waitTime = 3;
    // public Vector3 _casterTargetFxImpactOffset;

}