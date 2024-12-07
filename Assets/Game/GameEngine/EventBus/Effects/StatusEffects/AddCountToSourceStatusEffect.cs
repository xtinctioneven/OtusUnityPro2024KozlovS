using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    public class AddCountToSourceStatusEffect : IConsumeStatusEffect
    {
        [field: SerializeField, PropertyRange(0, 10)] public int AddCounts { get; private set; } = 1;
        [field: SerializeField] public StatusEffectType StatusEffectType { get; private set; }
        public EntityInteractionData InteractionData { get; set; }
        public IEntity SourceEntity { get; set; }
        public IEntity AfflictedEntity { get; set; }
        
        public IStatusEffect Clone()
        {
            var clone = new AddCountToSourceStatusEffect();
            clone.AddCounts = this.AddCounts;
            clone.StatusEffectType = this.StatusEffectType;
            return clone;
        }
    }
}