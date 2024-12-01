using Zenject;

public class StartRoundTask : EventTask
{
    private readonly EventBus _eventBus;
    private DiContainer _diContainer;
    
    public StartRoundTask(
        DiContainer diContainer,
        EventBus eventBus)
    {
        _eventBus = eventBus;
        _diContainer = diContainer;
    }

    protected override void OnRun()
    {
        // _activeHeroEntity = _turnOrderService.ActivateNextHero();
        //Reset UseCounts
        var entityTracker = _diContainer.Resolve<EntityTrackerService>();
        var allEntities = entityTracker.GetAllEntities();
        foreach (var entity in allEntities)
        {
            entity.GetEntityComponent<AbilityComponent>().ResetCounts();
        }
        //TODO: Fire RoundStartEvent
        Finish();
    }
}