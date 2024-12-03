using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;
using Zenject;
using IEffectPassive = Game.Gameplay.IEffectPassive;

public class StandardActionTask : EventTask
{
    private readonly DiContainer _diContainer;
    private readonly EventBus _eventBus;

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
        var entityInteractionService = _diContainer.Resolve<EntityInteractionService>();
        var battlefield = _diContainer.Resolve<BattlefieldModel>();
        var activeEntity = turnOrderService.GetActiveEntity();
        var sourceAbilityComponent = activeEntity.GetEntityComponent<AbilityComponent>();
        var targetFinderService = _diContainer.Resolve<TargetFinderService>();

        var triggerEffectsTracker = _diContainer.Resolve<TriggerEffectsTracker>();
        
        var standardAbilities = sourceAbilityComponent.GetAbilitiesByType<IEffectStandard>();
        foreach (var standardAbility in standardAbilities)
        {
            //standardAbility.SourceEntity = activeEntity;
            Vector2 position = activeEntity.GetEntityComponent<GridPositionComponent>().Value;
            Team activeTeam = activeEntity.GetEntityComponent<Game.Gameplay.TeamComponent>().Value;
            List<IEntity> targetsList = new List<IEntity>();
            while (standardAbility.CanBeUsed)
            {
                //Trigger before action abilities
                List<IEffectTrigger> effectTriggers = triggerEffectsTracker.GetTriggers(TriggerReason.BeforeAnyAction, activeEntity);
                //Use Trigger abilites
                for (int i = 0; i < effectTriggers.Count; i++)
                {
                    targetsList = targetFinderService.GetTargets(activeEntity, effectTriggers[i]);
                    for (int j = 0; j < targetsList.Count; j++)
                    {
                        IEntity targetEntity = targetsList[j];
                        entityInteractionService.SetTargetEntity(targetEntity);
                        EntityInteractionData interactionData = entityInteractionService.CreateCurrentInteractionData();
                        interactionData.SourceEffect = effectTriggers[i];
                        effectTriggers[i].InteractionData = interactionData;
                        _eventBus.RaiseEvent(effectTriggers[i]);
                        ApplyPassiveEffects(activeEntity, interactionData, sourceAbilityComponent);
                        ApplyPassiveEffects(targetEntity, interactionData, targetEntity.GetEntityComponent<AbilityComponent>());
                        _eventBus.RaiseEvent(new EntityInteractionEvent(effectTriggers[i].InteractionData));
                    }
                    effectTriggers[i].TryUseCount();
                }
                
                //Use Standard abilities
                targetsList = targetFinderService.GetTargets(activeEntity, standardAbility);
                for (int i = 0; i < targetsList.Count; i++)
                {
                    IEntity targetEntity = targetsList[i];
                    entityInteractionService.SetTargetEntity(targetEntity);
                    EntityInteractionData interactionData = entityInteractionService.CreateCurrentInteractionData();
                    interactionData.SourceEffect = standardAbility;
                    standardAbility.InteractionData = interactionData;
                    _eventBus.RaiseEvent(standardAbility);
                    ApplyPassiveEffects(activeEntity, interactionData, sourceAbilityComponent);
                    ApplyPassiveEffects(targetEntity, interactionData, targetEntity.GetEntityComponent<AbilityComponent>());
                    _eventBus.RaiseEvent(new EntityInteractionEvent(standardAbility.InteractionData));
                }
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
            //passiveAbility.SourceEntity = sourceEntity;
            passiveAbility.InteractionData = interactionData;
            _eventBus.RaiseEvent(passiveAbility);
        }
    }
    
}