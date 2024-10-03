using UnityEngine;

public struct StealHealthOnAttackEffect : IEffect
{
    [Range(0, 100)]public int EffectProbability;
    [Range(0, 10f)]public float DamageConvertCoefficient;
    public HeroInteractionData HeroInteractionData { get; set; }
    public HeroEntity SourceHero { get; set; }
    [field: SerializeField] public TurnPhase ActivateTurnPhase { get; set; }
    [field: SerializeField] public VfxData VfxAbilityData { get; set; }
}