using System;
using UnityEngine.Serialization;

[Serializable]
public class AbilityVisualData
{
    // public GameObject _casterFx;
    // public bool _parentCasterFx;
    // public GameObject _targetFx;
    // public GameObject _targetGroundFx;
    // public GameObject _casterTargetFx;
    // public GameObject _casterTargetImpactFx;
    // public string _casterTargetTransform;
    // public string _targetTargetTransform;
    // public float _targetFxDelay;
    // public float _casterFxDelay;
    public AbilityCastType CastType;
    public AnimationClipType SourceAnimationClip;  
    public AnimationClipType TargetAnimationClip;
    // public string _impactAnimation = "Impact";
    // public float _casterTargetFxSpeed = 1;
    // public float _waitTime = 3;
    // public Vector3 _casterTargetFxImpactOffset;

    public enum AbilityCastType
    {
        Melee = 10,
        FromPlace = 20,
        SameRowMelee = 30,
        SameRowRanged = 40
    }
    
    public enum AnimationClipType
    {
        AttackStrike,
        AttackRange,
        AttackCast,
        Dodge,
        Hit,
        LowFloat,
        HighFloat,
        Repulse,
        Death,
        Hi,
        Knockdown,
        Resurrect
    }
}