public class ActivateHeroHandler: BaseHandler<ActivateHeroEvent>
{
    private readonly HeroInteractionService _heroInteractionService;
    
    public ActivateHeroHandler(EventBus eventBus, HeroInteractionService heroInteractionService) : base(eventBus)
    {
        _heroInteractionService = heroInteractionService;
    }

    protected override void OnHandleEvent(ActivateHeroEvent evt)
    {
        _heroInteractionService.SetActiveHero(evt.HeroEntity);
    }
}