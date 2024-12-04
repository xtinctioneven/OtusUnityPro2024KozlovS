using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;
using Zenject;
using IEffectPassive = Game.Gameplay.IEffectPassive;

public class StandardActionTask : EventTask
{
    private readonly DiContainer _diContainer;
    private readonly EventBus _eventBus;
    private EntityInteractionService _entityInteractionService;
    private TargetFinderService _targetFinderService;
    private AbilityService _abilityService;

    public StandardActionTask(
        DiContainer diContainer,
        EventBus eventBus)
    {
        _eventBus = eventBus;
        _diContainer = diContainer;
    }

    protected override void OnRun()
    {
        Debug.Log("StandardActionTask.OnRun");
        var turnOrderService = _diContainer.Resolve<TurnOrderService>();
        _entityInteractionService = _diContainer.Resolve<EntityInteractionService>();
        _targetFinderService = _diContainer.Resolve<TargetFinderService>();
        _abilityService = _diContainer.Resolve<AbilityService>();
        var activeEntity = turnOrderService.GetActiveEntity();
        var sourceAbilityComponent = activeEntity.GetEntityComponent<AbilityComponent>();
        var standardAbilities = sourceAbilityComponent.GetAbilitiesByType<IEffectStandard>();
        var linkEffectsTracker = _diContainer.Resolve<LinkEffectsTracker>();
        foreach (var standardAbility in standardAbilities)
        {
            while (standardAbility.CanBeUsed)
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
                    //effectTriggers[i].TryUseCount();
                }
                
                //Use Standard ability
                _abilityService.UseAbility(activeEntity, standardAbility);
                standardAbility.TryUseCount();
            }
        }
        Finish();
    }
    
}