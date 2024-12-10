using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;
using Zenject;
using IEffectPassive = Game.Gameplay.IEffectPassive;

public class EntityActionTask : EventTask
{
    private readonly TurnOrderService _turnOrderService;
    private readonly AbilityService _abilityService;
    private readonly VisualPipeline _visualPipeline;

    public EntityActionTask(
        AbilityService abilityService,
        TurnOrderService turnOrderService,
        VisualPipeline visualPipeline
        )
    {
        _abilityService = abilityService;
        _turnOrderService = turnOrderService;
        _visualPipeline = visualPipeline;
    }

    protected override void OnRun()
    {
        Debug.Log("StandardActionTask.OnRun");
        var activeEntity = _turnOrderService.GetActiveEntity();
        var sourceAbilityComponent = activeEntity.GetEntityComponent<AbilityComponent>();
        var standardAbilities = sourceAbilityComponent.GetAbilitiesByType<IEffectStandard>();
        foreach (var standardAbility in standardAbilities)
        {
            while (standardAbility.CanBeUsed && !Helper.Instance.IsGameOver)
            {
                //Trigger before action abilities
                List<IEffectTrigger> effectTriggers = sourceAbilityComponent.GetAbilitiesByType<IEffectTrigger>();
                //Use Trigger abilites
                for (int i = 0; i < effectTriggers.Count; i++)
                {
                    if (effectTriggers[i].TriggerReason != TriggerReason.BeforeAnyAction || !effectTriggers[i].CanBeUsed)
                    {
                        continue;
                    }
                    _abilityService.UseAbility(activeEntity, effectTriggers[i]);
                }
                //Use Standard ability
                _abilityService.UseAbility(activeEntity, standardAbility);
            }
        }
        Finish();
    }
    
}