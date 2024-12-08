using System;
using Sirenix.OdinInspector;

namespace Game.Gameplay
{
    public interface IStickyStatusEffect : IStatusEffect
    {
        public string Name { get;}
        public StatusEffectType StatusEffectType { get;}
        public InteractionType InteractionType { get; }
        public bool IsCleanseable { get;}
        [PropertyRange(0, 9)] public int EffectDuration { get;}
        public int DurationLeft { get;}
        public void Tick(int tickAmount = 1);
    }
}