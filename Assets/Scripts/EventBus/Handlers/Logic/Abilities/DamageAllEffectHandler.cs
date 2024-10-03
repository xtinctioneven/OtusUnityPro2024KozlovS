using System.Collections.Generic;

public class DamageAllEffectHandler: BaseHandler<DamageAllEffect>
{
    private readonly HeroInteractionService _heroInteractionService;
    private readonly HeroTrackerService _heroTrackerService;
    private readonly VisualPipeline _visualPipeline;
    
    public DamageAllEffectHandler(EventBus eventBus, HeroInteractionService heroInteractionService,
        HeroTrackerService heroTrackerService, VisualPipeline visualPipeline) : base(eventBus)
    {
        _heroInteractionService = heroInteractionService;
        _heroTrackerService = heroTrackerService;
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(DamageAllEffect evt)
    {
        HeroEntity sourceHero = evt.SourceHero;
        HeroInteractionData interactionData = evt.HeroInteractionData;
        if (sourceHero == null || evt.HeroInteractionData == null)
        {
            return;
        }

        if (!((interactionData.SourceHero == sourceHero && interactionData.SourceHeroDamageReceived > 0)
              || (interactionData.TargetHero == sourceHero && interactionData.TargetHeroDamageReceived > 0)))
        {
            return;
        }
        
        List<HeroEntity> targetHeroes = new();
        targetHeroes.AddRange(_heroTrackerService.GetRedTeam());
        targetHeroes.AddRange(_heroTrackerService.GetBlueTeam());
        foreach (HeroEntity hero in targetHeroes)
        {
            HeroInteractionData effectInteractionData = _heroInteractionService.CreateEmptyInteractionData(sourceHero, hero);
            effectInteractionData.TargetHeroDamageReceived = evt.DamageValue;
            //Здесь иду в обход HeroInteractionEvent, чтобы не получилось самоубийственного/вечного цикла
            if (hero == sourceHero)
            {
                EventBus.RaiseEvent(new HeroStatsResolveEvent(sourceHero, effectInteractionData));
                continue;
            }
            EventBus.RaiseEvent(new HeroInteractionEvent(effectInteractionData));
        }
        if (evt.VfxAbilityData.Vfx)
        {
            _visualPipeline.AddTask(new PlayVfxTask(evt.VfxAbilityData, sourceHero));
        }
    }
}