public class NullifyRetaliateDamageEffectHandler: BaseHandler<NullifyRetaliateDamageEffect>
{
    private readonly VisualPipeline _visualPipeline;
    public NullifyRetaliateDamageEffectHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(NullifyRetaliateDamageEffect evt)
    {
        if (evt.SourceHero != evt.HeroInteractionData.SourceHero || evt.SourceHero == null)
        {
            return;
        }
        evt.HeroInteractionData.SourceHeroDamageReceived = 0;
        if (evt.VfxAbilityData.Vfx)
        {
            _visualPipeline.AddTask(new PlayVfxTask(evt.VfxAbilityData, evt.SourceHero));
        }
    }
}