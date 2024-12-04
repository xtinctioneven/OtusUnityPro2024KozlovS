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
        var activeEntity = turnOrderService.GetActiveEntity();
        var sourceAbilityComponent = activeEntity.GetEntityComponent<AbilityComponent>();
        var standardAbilities = sourceAbilityComponent.GetAbilitiesByType<IEffectStandard>();
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
                    UseAbility(activeEntity, effectTriggers[i]);
                    effectTriggers[i].TryUseCount();
                }
                
                //Use Standard ability
                UseAbility(activeEntity, standardAbility);
                standardAbility.TryUseCount();
            }
        }
        Finish();
    }

    private void ApplyPassiveEffects(IEntity sourceEntity, EntityInteractionData interactionData, AbilityComponent abilityComponent)
    {
        var passiveAbilities = abilityComponent.GetAbilitiesByType<IEffectPassive>();
        foreach (var passiveAbility in passiveAbilities)
        {
            if (!passiveAbility.CanBeUsed)
            {
                continue;
            }
            passiveAbility.InteractionData = interactionData;
            _eventBus.RaiseEvent(passiveAbility);
        }
    }

    private void UseAbility(IEntity activeEntity, IEffect ability)
    {
        List<IEntity> targetsList = _targetFinderService.GetTargets(activeEntity, ability as IEffectTarget);
        for (int i = 0; i < targetsList.Count; i++)
        {
            IEntity targetEntity = targetsList[i];
            _entityInteractionService.SetTargetEntity(targetEntity);
            EntityInteractionData interactionData = _entityInteractionService.CreateCurrentInteractionData();
            interactionData.SourceEffect = ability;
            ability.InteractionData = interactionData;
            _eventBus.RaiseEvent(ability);
            ApplyPassiveEffects(activeEntity, interactionData, activeEntity.GetEntityComponent<AbilityComponent>());
            ApplyPassiveEffects(targetEntity, interactionData, targetEntity.GetEntityComponent<AbilityComponent>());
            _eventBus.RaiseEvent(new EntityInteractionEvent(ability.InteractionData));
        }
    }
    
}