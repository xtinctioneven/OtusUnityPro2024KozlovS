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
    public AbilityCastType CastType = AbilityCastType.Melee;
    public AbilityCollisionType CollisionType = AbilityCollisionType.None;
    public AnimationClipType SourceAnimationClip = AnimationClipType.AttackStrike1;  
    // public AnimationClipType TargetAnimationClip = AnimationClipType.Hit;
    public float MeelePositionOffset = 1f;
    public float CastCollisionDelay = .6f;
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
    public enum AbilityCollisionType
    {
        None = 0,
        Strike = 10,
        Cast = 20,
    }
    
    public enum AnimationClipType
    {
        None,
        AttackStrike1,
        AttackStrike2,
        AttackRange1,
        AttackRange2,
        AttackLink1,
        AttackLink2,
        Cast1,
        Cast2,
        Dodge,
        Hit,
        LowFloat,
        HighFloat,
        Repulse,
        Death,
        Hi,
        Resurrect,
        MoveBack,
        MoveForward
    }
}