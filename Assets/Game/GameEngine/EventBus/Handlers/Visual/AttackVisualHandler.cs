using UI;

public class AttackVisualHandler : BaseHandler<AttackEvent>
{
    private readonly VisualPipeline _visualPipeline;
    
    public AttackVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(AttackEvent evt)
    {
        HeroView attackerView = evt.SourceHero.GetHeroComponent<ViewComponentOld>().Value;
        HeroView targetView = evt.TargetHero.GetHeroComponent<ViewComponentOld>().Value;
        _visualPipeline.AddTask(new AnimateAttackVisualTask(attackerView, targetView));
    }
}