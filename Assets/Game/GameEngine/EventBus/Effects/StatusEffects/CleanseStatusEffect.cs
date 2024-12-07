using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    public class CleanseStatusEffect : IConsumeStatusEffect
    {
        [field: SerializeField, PropertyRange(0, 10)] public int CleanseStatusesCount { get; set; } = 1;
        [field: SerializeField] public StatusEffectType CleanseStatusType { get; private set; } = StatusEffectType.Negative;
        [field: SerializeField] public StatusEffectType StatusEffectType { get; private set; } = StatusEffectType.Positive;
        public EntityInteractionData InteractionData { get; set; }
        public IEntity SourceEntity { get; set; }
        public IEntity AfflictedEntity { get; set; }
        
        public IStatusEffect Clone()
        {
            var clone = new CleanseStatusEffect();
            clone.CleanseStatusesCount = this.CleanseStatusesCount;
            clone.CleanseStatusType = this.CleanseStatusType;
            clone.StatusEffectType = this.StatusEffectType;
            return clone;
        }
    }
}