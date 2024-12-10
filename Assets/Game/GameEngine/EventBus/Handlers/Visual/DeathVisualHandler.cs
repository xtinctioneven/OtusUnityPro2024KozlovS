using Game.Gameplay;

public class DeathVisualHandler: BaseHandler<DeathEvent>
{
    private readonly VisualPipeline _visualPipeline;
    
    public DeathVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(DeathEvent evt)
    { 
        IEntity entity = evt.Entity;
        _visualPipeline.AddTask(new EntityDeathVisualTask(entity));
    }
}