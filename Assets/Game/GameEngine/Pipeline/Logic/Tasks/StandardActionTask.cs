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
        
        foreach (var standardAbility in sourceAbilityComponent.GetAbilitiesByType<IEffectStandard>())
        {
            standardAbility.SourceEntity = activeEntity;
            Vector2 position = activeEntity.GetEntityComponent<PositionComponent>().Value;
            Team activeTeam = activeEntity.GetEntityComponent<Game.Gameplay.TeamComponent>().Value;
            while (standardAbility.CanBeUsed)
            {
                List<IEntity> targetsList = battlefield.GetTargets(activeTeam, position, standardAbility.TargetType);
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
            passiveAbility.SourceEntity = sourceEntity;
            passiveAbility.InteractionData = interactionData;
            _eventBus.RaiseEvent(passiveAbility);
        }
    }
    
}