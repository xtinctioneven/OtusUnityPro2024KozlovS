using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    public class AddCountToStandardTriggerEffect : IEffectTrigger
    {
        //Fields to clone
        [PropertyRange(0, 10)] public int CountsToAdd;
        [field: SerializeField] public TriggerReason TriggerReason { get; private set; }
        [field: SerializeField] public InteractionType InteractionType { get; private set; } = InteractionType.Buff;
        // [field: SerializeField] public AbilityTraits Traits { get; private set; }
        [field: SerializeField] public TargetType TargetType { get; private set; } = TargetType.Self;
        [field: SerializeField] public TargetPriorityType TargetPriority { get; private set; } = TargetPriorityType.None;
        [field: SerializeField] public int TargetsCount { get; private set; } = 1;
        
        public IEffect Clone()
        {
            var clone = new AddCountToStandardTriggerEffect();
            clone.CountsToAdd = CountsToAdd;
            clone.TriggerReason = TriggerReason;
            clone.InteractionType = InteractionType;
            // clone.Traits = Traits;
            clone.TargetType = TargetType;
            clone.TargetPriority = TargetPriority;
            clone.TargetsCount = TargetsCount;
            clone.Enabled = true;
            return clone;
        }
        
        //Non-clone fields
        public IEntity SourceEntity { get; set; }
        public EntityInteractionData InteractionData { get; set; }
        public bool Enabled { get; private set; }
        bool IEffect.CanBeUsed => Enabled;

        //Self methods
        
        //interface methods
        public void Enable()
        {
            Enabled = true;
        }

        public void Disable()
        {
            Enabled = false;
        }
    }
}