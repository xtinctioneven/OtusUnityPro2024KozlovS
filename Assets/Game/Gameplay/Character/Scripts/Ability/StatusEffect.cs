using System;

namespace Game.Gameplay
{
    [Serializable]
    public class StatusEffect : IStatusEffect
    {
        public StatusEffectType StatusEffectType;
        public int EffectDuration;
    }
}