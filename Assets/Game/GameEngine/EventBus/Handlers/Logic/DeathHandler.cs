using Game.Gameplay;
using Zenject;

public class DeathHandler: BaseHandler<DeathEvent>
{
    //private EntityTrackerService _entityTrackerService;
    private DiContainer _diContainer;
    
    public DeathHandler(EventBus eventBus, DiContainer diContainer) : base(eventBus)
    {
        _diContainer = diContainer;
    }

    protected override void OnHandleEvent(DeathEvent evt)
    {
        var entityTracker = _diContainer.Resolve<EntityTrackerService>();
        IEntity entity = evt.Entity;
        entity.GetEntityComponent<DeathComponent>().Die();
        entityTracker.UntrackEntity(entity);
    }
}