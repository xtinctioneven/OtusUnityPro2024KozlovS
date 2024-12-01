using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StealHealthOnAttackEffectHandler: BaseHandler<StealHealthOnAttackEffectOld>
{
    private readonly HeroInteractionService _heroInteractionService;
    private readonly VisualPipeline _visualPipeline;
    
    public StealHealthOnAttackEffectHandler(EventBus eventBus, HeroInteractionService heroInteractionService,
        VisualPipeline visualPipeline) : base(eventBus)
    {
        _heroInteractionService = heroInteractionService;
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(StealHealthOnAttackEffectOld evt)
    {
        HeroInteractionData currentClashData = evt.HeroInteractionData;
        if (evt.EffectProbability < Random.Range(0, 100) || currentClashData.SourceHero != evt.SourceHero || evt.SourceHero == null)
        {
            return;
        }
        HeroInteractionData effectInteractionData = _heroInteractionService.CreateEmptyInteractionData();

        int damageDone = currentClashData.TargetHeroDamageReceived;
        int resultHeal = (int)Mathf.Floor((float)damageDone * evt.DamageConvertCoefficient);
        effectInteractionData.SourceHeroDamageReceived -= resultHeal;
        if (evt.VfxAbilityData.Vfx)
        {
            _visualPipeline.AddTask(new PlayVfxTask(evt.VfxAbilityData, effectInteractionData.SourceHero, effectInteractionData.TargetHero));
        }
        // EventBus.RaiseEvent(new HeroInteractionEvent(effectInteractionData));
    }
}