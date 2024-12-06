using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay
{
    [Serializable]
    public struct StatusEffectData
    {
        [SerializeReference] public StatusEffectConfig[] StatusEffectsApplyToTarget;
        [SerializeReference] public StatusEffectConfig[] StatusEffectsApplyToSelf;
        [PropertyRange(0, 1)] public float EffectProbability;
    }
}