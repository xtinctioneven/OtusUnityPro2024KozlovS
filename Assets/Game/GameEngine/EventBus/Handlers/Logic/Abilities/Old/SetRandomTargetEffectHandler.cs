using System.Collections.Generic;
using UnityEngine;

public class SetRandomTargetEffectHandler: BaseHandler<SetRandomTargetEffectOld>
{
    private readonly HeroTrackerService _heroTrackerService;
    private readonly VisualPipeline _visualPipeline;
    
    public SetRandomTargetEffectHandler(EventBus eventBus, 
        HeroTrackerService heroTrackerService, VisualPipeline visualPipeline) : base(eventBus)
    {
        _heroTrackerService = heroTrackerService;
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(SetRandomTargetEffectOld evt)
    {
        HeroInteractionData heroInteractionData = evt.HeroInteractionData;
        if (evt.EffectProbability < Random.Range(0, 100) || evt.SourceHero != heroInteractionData.SourceHero || evt.SourceHero == null)
        {
            return;
        }
        // Team targetTeam = heroInteractionData.TargetHero.GetHeroComponent<TeamComponent>().Value;
        // List<HeroEntity> possibleTargets = new List<HeroEntity>();
        // if (targetTeam == Team.RedTeam)
        // {
            // foreach (var possibleTarget in _heroTrackerService.GetRedTeam())
            // {
                // if (possibleTarget != heroInteractionData.TargetHero)
                // {
                    // possibleTargets.Add(possibleTarget);
                // }
            // }
        // }
        // else
        // {
            // foreach (var possibleTarget in _heroTrackerService.GetBlueTeam())
            // {
                // if (possibleTarget != heroInteractionData.TargetHero)
                // {
                    // possibleTargets.Add(possibleTarget);
                // }
            // }
        // }
        // if (possibleTargets.Count < 1)
        // {
            // return;
        // }
        // int index = Random.Range(0, possibleTargets.Count);
        // heroInteractionData.TargetHero = possibleTargets[index];
        // if (evt.VfxAbilityData.Vfx)
        // {
            // _visualPipeline.AddTask(new PlayVfxTask(evt.VfxAbilityData, heroInteractionData.SourceHero));
        // }
    }
}