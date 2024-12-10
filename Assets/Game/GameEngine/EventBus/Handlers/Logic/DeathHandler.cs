using Game.Gameplay;
using Zenject;

public class DeathHandler: BaseHandler<DeathEvent>
{
    private EntityTrackerService _entityTrackerService;
    
    public DeathHandler(EventBus eventBus, EntityTrackerService entityTrackerService) : base(eventBus)
    {
        _entityTrackerService = entityTrackerService;
    }

    protected override void OnHandleEvent(DeathEvent evt)
    {
        IEntity entity = evt.Entity;
        entity.GetEntityComponent<DeathComponent>().Die();
        _entityTrackerService.UntrackEntity(entity);
    }
}