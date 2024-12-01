using System;

namespace Game.Gameplay
{
    public interface IEffect : IEvent
    {
        IEntity SourceEntity { get; set; }
        EntityInteractionData InteractionData { get; set; }
        InteractionType InteractionType {get; set;}
        public AbilityTraits Traits { get; set; }
        //TurnPhase ActivateTurnPhase { get; set; }
        //VfxData VfxAbilityData { get; set; }
        bool CanBeUsed { get; }
        bool Enabled { get; }
        public void Enable();
        public void Disable();
        public IEffect Clone();
    }
    
    [Flags]
    public enum AbilityTraits
    {
        None = 0,
        Melee = 1,
        Ranged = 2,
        Heal = 4,
        Physical = 8,
    }
}