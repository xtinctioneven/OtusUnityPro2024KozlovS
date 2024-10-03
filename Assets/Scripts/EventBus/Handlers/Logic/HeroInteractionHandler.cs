public class HeroInteractionHandler: BaseHandler<HeroInteractionEvent>
{
    public HeroInteractionHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(HeroInteractionEvent evt)
    {
        HeroInteractionData interactionData = evt.HeroInteractionData;
        HeroEntity sourceHero = evt.HeroInteractionData.SourceHero;
        HeroEntity targetHero = evt.HeroInteractionData.TargetHero;
        EventBus.RaiseEvent(new ActivateAbilitiesEvent(TurnPhase.HeroesInteraction, sourceHero, interactionData));
        EventBus.RaiseEvent(new ActivateAbilitiesEvent(TurnPhase.HeroesInteraction, targetHero, interactionData));
        EventBus.RaiseEvent(new HeroStatsResolveEvent(sourceHero, interactionData));
        EventBus.RaiseEvent(new HeroStatsResolveEvent(targetHero, interactionData));
    }
}