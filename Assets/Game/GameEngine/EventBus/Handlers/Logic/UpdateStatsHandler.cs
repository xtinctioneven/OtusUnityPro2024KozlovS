using Game.Gameplay;

public class UpdateStatsHandler: BaseHandler<UpdateStatsEvent>
{
    public UpdateStatsHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(UpdateStatsEvent evt)
    {
        IEntity entity = evt.Entity;
        entity.GetEntityComponent<HealthComponent>().Value = evt.HPValue;
        if (evt.HPValue <= 0) 
        {
            EventBus.RaiseEvent(new DeathEvent(entity));
        }
    }
}