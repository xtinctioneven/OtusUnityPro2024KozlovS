public class TargetHeroClickedHandler: BaseHandler<TargetHeroClickedEvent>
{
    private readonly HeroInteractionService _heroInteractionService;
    
    public TargetHeroClickedHandler(EventBus eventBus, HeroInteractionService heroInteractionService) : base(eventBus)
    {
        _heroInteractionService = heroInteractionService;
    }

    protected override void OnHandleEvent(TargetHeroClickedEvent evt)
    {
        HeroEntity heroEntity = evt.HeroView.GetComponent<HeroEntity>();
        _heroInteractionService.SetTargetHero(heroEntity);
    }
}