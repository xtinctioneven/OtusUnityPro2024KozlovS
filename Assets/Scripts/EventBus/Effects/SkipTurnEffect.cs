using UnityEngine;

public struct SkipTurnEffect : IEffect
{
    public HeroEntity SourceHero { get; set; }
    public HeroInteractionData HeroInteractionData { get; set; }
    [field: SerializeField] public TurnPhase ActivateTurnPhase { get; set; }
    [field: SerializeField] public VfxData VfxAbilityData { get; set; }
}