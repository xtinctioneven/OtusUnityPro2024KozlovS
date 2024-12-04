using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay
{
    [Serializable]
    public struct StatusEffectData
    {
        [SerializeReference] public IStatusEffect[] StatusEffects;
        [PropertyRange(0, 1)] public float EffectProbability;
    }
}