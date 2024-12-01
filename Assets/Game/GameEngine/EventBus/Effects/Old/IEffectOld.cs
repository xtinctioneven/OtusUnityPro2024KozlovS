using UnityEngine;

public interface IEffectOld : IEvent
{
    HeroInteractionData HeroInteractionData { get; set; }
    HeroEntity SourceHero { get; set; }
    TurnPhase ActivateTurnPhase { get; set; }
    VfxData VfxAbilityData { get; set; }
    
}