using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    public class StrikeTargetEffect : IEffectStandard, IEffectApplyStatus
    {
        //Self fields
        [PropertyRange(0, 10)] public float AttackMultiplier;

        //interface fields
        [field: SerializeField] public TargetType TargetType { get; set; }
        [field: SerializeField] public TargetPriorityType Priority { get; set; }
        [field: SerializeField] public StatusEffectData[] StatusEffects { get; set; }
        [field: SerializeField, PropertyRange(0, 10)] public int InitialUseCounts { get; private set; }
        [field: SerializeField, PropertyRange(0, 10)] public int MaxUseCounts { get; private set; }
        public EntityInteractionData InteractionData { get; set; }
        [field: SerializeField] public AbilityTraits Traits { get; set; }
        [field: SerializeField] public InteractionType InteractionType { get; set; }
        public int CountsUsed { get; private set; }
        public int CountsLeft { get; private set; }
        public IEntity SourceEntity { get; set; }
        public bool Enabled { get; private set; }
        public IEntity[] TargetEntities { get; set; }

        //Self methods
        
        //interface methods
        
        public IEffect Clone()
        {
            var clone = new StrikeTargetEffect();
            clone.AttackMultiplier = AttackMultiplier;
            clone.TargetType = TargetType;
            clone.Priority = Priority;
            clone.Traits = Traits;
            clone.InteractionType = InteractionType;
            clone.StatusEffects = StatusEffects;
            clone.InitialUseCounts = InitialUseCounts;
            clone.MaxUseCounts = MaxUseCounts;
            clone.Traits = Traits;
            clone.CountsUsed = 0;
            clone.CountsLeft = InitialUseCounts;
            clone.SourceEntity = null;
            clone.Enabled = true;
            return clone;
        }
        
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