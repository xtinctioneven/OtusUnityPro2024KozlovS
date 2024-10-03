public class DeathHandler: BaseHandler<DeathEvent>
{
    private HeroTrackerService _heroTrackerService;
    
    public DeathHandler(EventBus eventBus, HeroTrackerService heroTrackerService) : base(eventBus)
    {
        _heroTrackerService = heroTrackerService;
    }

    protected override void OnHandleEvent(DeathEvent evt)
    {
        HeroEntity heroEntity = evt.HeroEntity;
        heroEntity.GetHeroComponent<DeathComponent>().Die();
        _heroTrackerService.RemoveHero(heroEntity);
    }
}