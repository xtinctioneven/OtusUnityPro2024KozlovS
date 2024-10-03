using System.Collections.Generic;
using UnityEngine;

public class DamageRandomEnemyEffectHandler : BaseHandler<DamageRandomEnemyEffect>
{
    private readonly HeroInteractionService _heroInteractionService;
    private readonly HeroTrackerService _heroTrackerService;
    private readonly VisualPipeline _visualPipeline;

    public DamageRandomEnemyEffectHandler(EventBus eventBus, HeroInteractionService heroInteractionService,
        HeroTrackerService heroTrackerService, VisualPipeline visualPipeline) : base(eventBus)
    {
        _heroInteractionService = heroInteractionService;
        _heroTrackerService = heroTrackerService;
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(DamageRandomEnemyEffect evt)
    {
        if (evt.SourceHero != evt.HeroInteractionData.SourceHero || evt.SourceHero == null)
        {
            return;
        }
        HeroInteractionData effectInteractionData = _heroInteractionService.CreateEmptyInteractionData();
        Team targetTeam = effectInteractionData.TargetHero.GetHeroComponent<TeamComponent>().Value;
        IReadOnlyList<HeroEntity> possibleTargets;
        if (targetTeam == Team.RedTeam)
        {
            possibleTargets = _heroTrackerService.GetRedTeam();
        }
        else
        {
            possibleTargets = _heroTrackerService.GetBlueTeam();
        }
        if (possibleTargets.Count < 1)
        {
            return;
        }
        int index = Random.Range(0, possibleTargets.Count);
        effectInteractionData.TargetHero = possibleTargets[index];
        effectInteractionData.TargetHeroDamageReceived = evt.DamageValue;
        if (evt.VfxAbilityData.Vfx)
        {
            _visualPipeline.AddTask(new PlayVfxTask(evt.VfxAbilityData, effectInteractionData.SourceHero,
                effectInteractionData.TargetHero));
        }

        EventBus.RaiseEvent(new HeroInteractionEvent(effectInteractionData));
    }
}