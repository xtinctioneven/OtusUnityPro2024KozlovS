using UI;

public class DeactivateHeroVisualHandler: BaseHandler<DeactivateHeroEvent>
{
    private readonly VisualPipeline _visualPipeline;
    
    public DeactivateHeroVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(DeactivateHeroEvent evt)
    {
        HeroView heroView = evt.HeroEntity.GetHeroComponent<ViewComponent>().Value;
        _visualPipeline.AddTask(new DeactivateHeroVisualTask(heroView));
    }
}