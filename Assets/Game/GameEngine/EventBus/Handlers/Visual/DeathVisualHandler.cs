public class DeathVisualHandler: BaseHandler<DeathEvent>
{
    private readonly VisualPipeline _visualPipeline;
    
    public DeathVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(DeathEvent evt)
    {
        // HeroEntity heroEntity = evt.HeroEntity;
        // foreach (var abilityVfxPair in heroEntity.GetHeroComponent<VfxComponent>().AbilityVfxDictionary)
        // {
            // _visualPipeline.AddTask(new DestoryVfxTask(abilityVfxPair.Value));
        // }
        // _visualPipeline.AddTask(new DestroyHeroVisualTask(heroEntity));
    }
}