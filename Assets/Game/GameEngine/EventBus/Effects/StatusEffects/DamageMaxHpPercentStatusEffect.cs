using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    public class DamageMaxHpPercentStatusEffect : IStickyStatusEffect
    {
        [field: SerializeField, PropertyRange(0, 1)] public float DamagePerTick { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public StatusEffectType StatusEffectType { get; private set; }
        [field: SerializeField] public bool IsCleanseable { get; private set; }
        [field: SerializeField] public int EffectDuration { get; private set; }
        public EntityInteractionData InteractionData { get; set; }
        public IEntity AfflictedEntity { get; set; }
        public int DurationLeft { get; private set; }
        public IEntity SourceEntity { get; set; }
        
        public IStatusEffect Clone()
        {
            var clone = new DamageMaxHpPercentStatusEffect();
            clone.DamagePerTick = DamagePerTick;
            clone.Name = Name;
            clone.StatusEffectType = StatusEffectType;
            clone.IsCleanseable = IsCleanseable;
            clone.EffectDuration = EffectDuration;
            clone.DurationLeft = EffectDuration;
            return clone;
        }

        public void Tick(int tickAmount = 1)
        {
            DurationLeft -= tickAmount;
        }
    }
}