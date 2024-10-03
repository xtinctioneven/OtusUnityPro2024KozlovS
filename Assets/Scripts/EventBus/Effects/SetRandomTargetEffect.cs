using UnityEngine;

public struct SetRandomTargetEffect : IEffect
{
    [Range(0, 100)]public int EffectProbability;
    public HeroEntity SourceHero { get; set; }
    public HeroInteractionData HeroInteractionData { get; set; }
    [field: SerializeField] public TurnPhase ActivateTurnPhase { get; set; }
    [field: SerializeField] public VfxData VfxAbilityData { get; set; }
}