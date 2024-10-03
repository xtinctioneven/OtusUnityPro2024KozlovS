public struct ActivateAbilitiesEvent : IEvent
{
    public TurnPhase TurnPhase;
    public HeroEntity SourceHero;
    public HeroInteractionData HeroInteractionData;

    public ActivateAbilitiesEvent(TurnPhase turnPhase, HeroEntity sourceHero, HeroInteractionData heroInteractionData = null)
    {
        TurnPhase = turnPhase;
        SourceHero = sourceHero;
        HeroInteractionData = heroInteractionData;
    }
}