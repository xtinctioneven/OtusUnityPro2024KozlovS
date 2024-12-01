using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    public class EvasionPassiveEffect : IEffectPassive
    {
        //Fields to clone
        [field: SerializeField] public List<InteractionType> InteractionTypesToDodge; 
        [field: SerializeField, PropertyRange(0, 1)] public float EvasionProbability;
        [field: SerializeField, PropertyRange(0, 10)] public int InitialUseCounts { get; private set; }
        [field: SerializeField, PropertyRange(0, 10)] public int MaxUseCounts { get; private set; }
        public int CountsUsed { get; private set; }
        public int CountsLeft { get; private set; }
        public EntityInteractionData InteractionData { get; set; }
        [field: SerializeField] public AbilityTraits Traits { get; set; }
        [field: SerializeField] public InteractionType InteractionType { get; set; }
        public IEntity SourceEntity { get; set; }
        public bool Enabled { get; private set; }

        public IEffect Clone()
        {
            var clone = new EvasionPassiveEffect();
            clone.InteractionTypesToDodge = InteractionTypesToDodge;
            clone.EvasionProbability = EvasionProbability;
            clone.InitialUseCounts = InitialUseCounts;
            clone.MaxUseCounts = MaxUseCounts;
            clone.CountsUsed = 0;
            clone.CountsLeft = InitialUseCounts;
            clone.Traits = Traits;
            clone.InteractionType = InteractionType;
            clone.SourceEntity = null;
            clone.Enabled = true;
            return clone;
        }

        //Other fields
        public IEntity[] TargetEntities { get; set; }

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