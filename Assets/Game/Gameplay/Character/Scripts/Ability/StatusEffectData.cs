using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public struct StatusEffectData
    {
        [SerializeReference] public IStatusEffect StatusEffect;
        [PropertyRange(0, 1)] public float EffectProbability;
    }
}