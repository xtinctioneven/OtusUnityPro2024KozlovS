using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    public class EvasionPassiveEffect : IEffectPassive, IEffectUseCounts
    {
        //Fields to clone
        [field: SerializeField] public List<InteractionResult> InteractionResultsToDodge { get; private set; }
        [field: SerializeField] public List<InteractionType> InteractionTypesToDodge { get; private set; } 
        [field: SerializeField, PropertyRange(0, 1)] public float EffectProbability { get; private set; }
        [field: SerializeField] public InteractionType InteractionType { get; private set; }
        [field: SerializeField, PropertyRange(0, 10)] public int InitialUseCounts { get; private set; }
        [field: SerializeField, PropertyRange(0, 10)] public int MaxUseCounts { get; private set; }
        public int CountsLeft { get; private set; }
        [field: SerializeField] public AbilityVisualData AbilityVisualData { get; private set; }

        public IEffect Clone()
        {
            var clone = new EvasionPassiveEffect();
            clone.InteractionResultsToDodge = InteractionResultsToDodge;
            clone.InteractionTypesToDodge = InteractionTypesToDodge;
            clone.EffectProbability = EffectProbability;
            clone.InteractionType = InteractionType;
            clone.InitialUseCounts = InitialUseCounts;
            clone.MaxUseCounts = MaxUseCounts;
            clone.CountsLeft = InitialUseCounts;
            clone.AbilityVisualData = AbilityVisualData;
            clone.Enabled = true;
            return clone;
        }

        //Non-clone fields
        public IEntity SourceEntity { get; set; }
        public EntityInteractionData InteractionData { get; set; }
        public int CountsUsed { get; private set; }
        public bool Enabled { get; private set; }
        bool IEffect.CanBeUsed => Enabled && CountsLeft > 0;

        //Self methods
        
        //interface methods
        public void AddCount(int addCount)
        {
            if (MaxUseCounts - addCount >= CountsUsed + CountsLeft)
            {
                CountsLeft += addCount;
            }
            else
            {
                CountsLeft += MaxUseCounts - CountsUsed - CountsLeft;
            }
        }

        public int SubtractCount(int subtractCount)
        {
            int subtractedCounts = 0;
            if (CountsLeft >= subtractCount)
            {
                CountsLeft -= subtractCount;
                subtractedCounts = subtractCount;
            }
            else
            {
                subtractedCounts = CountsLeft;
                CountsLeft = 0;
            }
            return subtractedCounts;
        }

        public bool TryUseCount(int count = 1)
        {
            if (CountsLeft >= count)
            {
                CountsLeft -= count;
                CountsUsed += count;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ResetCounts()
        {
            CountsUsed = 0;
            CountsLeft = InitialUseCounts;
        }
        
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