using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    public class AddCountToSourceOnResultPassiveEffect : IEffectPassive
    {
        //Fields to clone
        [field: SerializeField] public List<InteractionResult> InteractionResultsToTrigger { get; private set; }  
        [field: SerializeField] public List<InteractionType> InteractionTypesToTrigger { get; private set; }
        [field: SerializeField, PropertyRange(0, 1)] public float EffectProbability { get; private set; }
        [field: SerializeField] public int AddCountsOnTrigger { get; private set; }
        [field: SerializeField] public InteractionType InteractionType { get; private set; }
        // [field: SerializeField] public AbilityTraits Traits { get; private set; }
        
        public IEffect Clone()
        {
            var clone = new AddCountToSourceOnResultPassiveEffect();
            clone.InteractionResultsToTrigger = InteractionResultsToTrigger;
            clone.InteractionTypesToTrigger = InteractionTypesToTrigger;
            clone.EffectProbability = EffectProbability;
            clone.AddCountsOnTrigger = AddCountsOnTrigger;
            clone.InteractionType = InteractionType;
            // clone.Traits = Traits;
            clone.Enabled = true;
            return clone;
        }
        
        //Non-clone fields
        public IEntity SourceEntity { get; set; }
        public IEntity[] TargetEntities { get; set; }
        public EntityInteractionData InteractionData { get; set; }
        public bool Enabled { get; private set; }

        bool IEffect.CanBeUsed =>
            (InteractionData.SourceEffect != null
             && SourceEntity == InteractionData.SourceEntity
             && Enabled
             && InteractionTypesToTrigger.Contains(InteractionData.SourceEffect.InteractionType)
             && InteractionResultsToTrigger.Contains(InteractionData.InteractionResult)
            );

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