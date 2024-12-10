using Game.Gameplay;
using UnityEngine;

public class UpdateStatsVisualHandler : BaseHandler<UpdateStatsEvent>
{
    private readonly VisualPipeline _visualPipeline;
    
    public UpdateStatsVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(UpdateStatsEvent evt)
    {
        IEntity entity = evt.Entity;
        _visualPipeline.AddTask(new UpdateStatsVisualTask(entity.GetEntityComponent<HealthViewComponent>(), evt.HPValue));
    }
}