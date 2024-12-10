using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    public class StrikeTargetLinkEffect : IEffectLink, IEffectApplyStatus
    {
        //Fields to clone
        [PropertyRange(0, 10)] public float AttackMultiplier;
        [field: SerializeField] public LinkStatusType SeekLinkStatus { get; set; }
        [field: SerializeField] public InteractionType InteractionType { get; private set; } = InteractionType.LinkStrike;
        [field: SerializeField, PropertyRange(0, 10)] public int InitialUseCounts { get; private set; }
        [field: SerializeField, PropertyRange(0, 10)] public int MaxUseCounts { get; private set; }
        public int CountsLeft { get; private set; }
        [field: SerializeField] public TargetType TargetType { get; private set; } = TargetType.EnemyTarget;
        [field: SerializeField] public TargetPriorityType TargetPriority { get; private set; } = TargetPriorityType.None;
        [field: SerializeField, PropertyRange(1, 9)] public int TargetsCount { get; private set; } = 1;
        [field: SerializeField] public StatusEffectData[] StatusEffectsDataCollection { get; private set; }
        [field: SerializeField] public AbilityVisualData AbilityVisualData { get; private set; }
        
        public IEffect Clone()
        {
            var clone = new StrikeTargetLinkEffect();
            clone.AttackMultiplier = AttackMultiplier;
            clone.SeekLinkStatus = SeekLinkStatus;
            clone.InteractionType = InteractionType;
            // clone.Traits = Traits;
            clone.InitialUseCounts = InitialUseCounts;
            clone.MaxUseCounts = MaxUseCounts;
            clone.CountsLeft = InitialUseCounts;
            clone.TargetType = TargetType;
            clone.TargetPriority = TargetPriority;
            clone.TargetsCount = TargetsCount;
            clone.StatusEffectsDataCollection = StatusEffectsDataCollection;
            clone.AbilityVisualData = AbilityVisualData;
            clone.Enabled = true;
            return clone;
        }
        
        //Non-clone fields
        public IEntity SourceEntity { get; set; }
        public EntityInteractionData InteractionData { get; set; }
        public int CountsUsed { get; private set; }
        public bool Enabled { get; private set; }

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