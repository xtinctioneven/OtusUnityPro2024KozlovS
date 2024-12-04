using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class PersistentStatusEffect : IStatusEffect
    {
        public string Name { get; private set; }
        public StatusEffectType StatusEffectType;
        public int EffectDuration;
    }
}