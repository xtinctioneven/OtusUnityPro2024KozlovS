using UnityEngine;

public struct NullifyDamageEffect : IEffect
{
    public HeroEntity SourceHero { get; set; }
    public HeroInteractionData HeroInteractionData { get; set; }
    [field: SerializeField] public TurnPhase ActivateTurnPhase { get; set; }
    [field: SerializeField] public VfxData VfxAbilityData { get; set; }
    public AudioClip[] VfxSoundEffects { get; set; }
}