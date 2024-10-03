using UnityEngine;

public interface IEffect : IEvent
{
    HeroInteractionData HeroInteractionData { get; set; }
    HeroEntity SourceHero { get; set; }
    TurnPhase ActivateTurnPhase { get; set; }
    VfxData VfxAbilityData { get; set; }
    
}