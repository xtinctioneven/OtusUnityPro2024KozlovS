using System.Collections.Generic;
using UnityEngine;

public class HealRandomAllyEffectHandler: BaseHandler<HealRandomAllyEffect>
{
    private readonly HeroInteractionService _heroInteractionService;
    private readonly HeroTrackerService _heroTrackerService;
    private readonly VisualPipeline _visualPipeline;
    
    public HealRandomAllyEffectHandler(EventBus eventBus, HeroInteractionService heroInteractionService,
        HeroTrackerService heroTrackerService, VisualPipeline visualPipeline) : base(eventBus)
    {
        _heroInteractionService = heroInteractionService;
        _heroTrackerService = heroTrackerService;
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(HealRandomAllyEffect evt)
    {
        HeroInteractionData interactionData;
        if (evt.HeroInteractionData == null)
        {
            interactionData = _heroInteractionService.GetCurrentClashData();
        }
        else
        {
            interactionData = evt.HeroInteractionData;
        }
        HeroEntity sourceHero = evt.SourceHero;
        Team sourceTeam = sourceHero.GetHeroComponent<TeamComponent>().Value;
        if (evt.SourceHero == null || sourceTeam != interactionData.SourceHero.GetHeroComponent<TeamComponent>().Value)
        {
            return;
        }
        List<HeroEntity> possibleTargets = new();
        if (sourceTeam == Team.RedTeam)
        {
            foreach (var possibleTarget in _heroTrackerService.GetRedTeam())
            {
                if (possibleTarget != sourceHero)
                {
                    possibleTargets.Add(possibleTarget);
                }
            }
        }
        else
        {
            foreach (var possibleTarget in _heroTrackerService.GetBlueTeam())
            {
                if (possibleTarget != sourceHero)
                {
                    possibleTargets.Add(possibleTarget);
                }
            }
        }
        if (possibleTargets.Count < 1)
        {
            return;
        }
        int index = Random.Range(0, possibleTargets.Count);
        HeroInteractionData effectInteractionData = _heroInteractionService.CreateEmptyInteractionData(sourceHero, possibleTargets[index]);
        effectInteractionData.TargetHeroDamageReceived = -evt.HealValue;
        if (evt.VfxAbilityData.Vfx)
        {
            _visualPipeline.AddTask(new PlayVfxTask(evt.VfxAbilityData, effectInteractionData.SourceHero, effectInteractionData.TargetHero));
        }
        EventBus.RaiseEvent(new HeroInteractionEvent(effectInteractionData));
    }
}