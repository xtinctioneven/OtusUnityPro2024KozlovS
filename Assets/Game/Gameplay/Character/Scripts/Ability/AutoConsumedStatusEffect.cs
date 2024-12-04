using System;

namespace Game.Gameplay
{
    [Serializable]
    public class AutoConsumedStatusEffect : IStatusEffect
    {
        public string Name { get; private set; }
        public StatusEffectType StatusEffectType;
    }
}