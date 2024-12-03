using UnityEngine;

namespace Game.Gameplay
{
    public interface IEffect : IEvent
    {
        IEntity SourceEntity { get; set; }
        EntityInteractionData InteractionData { get; set; }
        InteractionType InteractionType {get; }
        public AbilityTraits Traits { get; }
        //TurnPhase ActivateTurnPhase { get; set; }
        //VfxData VfxAbilityData { get; set; }
        bool CanBeUsed { get; }
        bool Enabled { get; }
        public void Enable();
        public void Disable();
        public IEffect Clone();
    }
}