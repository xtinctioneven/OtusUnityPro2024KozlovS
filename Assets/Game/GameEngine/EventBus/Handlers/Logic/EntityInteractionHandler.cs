using System;
using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;
using Zenject;

public class EntityInteractionHandler: BaseHandler<EntityInteractionEvent>
{
    private AbilityService _abilityService;
    private DiContainer _diContainer;
    public EntityInteractionHandler(EventBus eventBus, DiContainer diContainer) : base(eventBus)
    {
        _diContainer = diContainer;
    }

    protected override void OnHandleEvent(EntityInteractionEvent evt)
    {
        _abilityService = _diContainer.Resolve<AbilityService>();
        EntityInteractionData interactionData = evt.EntityInteractionData;
        //Resolve interaction data
        ResolveInteractionData(interactionData);
        //Apply results
        //Source entity
        IEntity sourceEntity = interactionData.SourceEntity;
        _abilityService.StatusEffectsApply(sourceEntity, interactionData.StatusEffectsApplyToSource, interactionData);
        HealthComponent healthComponent = sourceEntity.GetEntityComponent<HealthComponent>();
        int entityHpResult = healthComponent.Value - interactionData.SourceEntityDamageReceived + interactionData.SourceEntityHealReceived;
        entityHpResult = Math.Clamp(entityHpResult, HealthComponent.MIN_LIFE, healthComponent.MaxValue);
        EventBus.RaiseEvent(new UpdateStatsEvent(sourceEntity, entityHpResult));
        //Target entity
        IEntity targetEntity = interactionData.TargetEntity;
        _abilityService.StatusEffectsApply(targetEntity, interactionData.StatusEffectsApplyToTarget, interactionData);
        healthComponent = targetEntity.GetEntityComponent<HealthComponent>();
        entityHpResult = healthComponent.Value - interactionData.TargetEntityDamageReceived + interactionData.TargetEntityHealReceived;
        entityHpResult = Math.Clamp(entityHpResult, HealthComponent.MIN_LIFE, healthComponent.MaxValue);
        if (entityHpResult == 0)
        {
            interactionData.InteractionResult = InteractionResult.Kill;
        }
        EventBus.RaiseEvent(new UpdateStatsEvent(targetEntity, entityHpResult));
        _abilityService.PassiveEffectsApply(interactionData, sourceEntity.GetEntityComponent<AbilityComponent>());
        _abilityService.PassiveEffectsApply(interactionData, targetEntity.GetEntityComponent<AbilityComponent>());
        SendLog(interactionData, entityHpResult);
    }

    private void ResolveInteractionData(EntityInteractionData interactionData)
    {
        DefenceComponent defenceComponent;
        defenceComponent = interactionData.SourceEntity.GetEntityComponent<DefenceComponent>();
        interactionData.SourceEntityDamageReceived = Math.Max(interactionData.TargetEntityDamageOutgoing - defenceComponent.Value, 0);
        switch (interactionData.InteractionResult)
        {
            case (InteractionResult.Hit):
            {
                defenceComponent = interactionData.TargetEntity.GetEntityComponent<DefenceComponent>();
                interactionData.TargetEntityDamageReceived = Math.Max(interactionData.SourceEntityDamageOutgoing - defenceComponent.Value, 0);
                
                break;
            }
            
            case (InteractionResult.Dodge):
            {
                interactionData.TargetEntityDamageReceived = 0;
                interactionData.StatusEffectsApplyToTarget.Clear();
                break;
            }
            
            case (InteractionResult.CriticalHit):
            {
                defenceComponent = interactionData.TargetEntity.GetEntityComponent<DefenceComponent>();
                interactionData.TargetEntityDamageReceived = Math.Max(0,
                    (int)(interactionData.SourceEntityDamageOutgoing * interactionData.CriticalDamageMultiplier - defenceComponent.Value));
                break;
            }

            case (InteractionResult.Heal):
            {
                int maxHeal = interactionData.TargetEntity.GetEntityComponent<HealthComponent>().HealthShortage;
                interactionData.TargetEntityHealReceived = Math.Min(interactionData.SourceEntityHealOutgoing, maxHeal);
                break;
            }

            case (InteractionResult.Default):
            {
                break;
            }
            
            case (InteractionResult.StatusEffectTick):
            {
                defenceComponent = interactionData.TargetEntity.GetEntityComponent<DefenceComponent>();
                interactionData.TargetEntityDamageReceived = Math.Max(interactionData.SourceEntityDamageOutgoing - defenceComponent.Value, 0);
                
                break;
            }

            default:
            {
                Debug.LogError($"Unhandled interaction result: {interactionData.InteractionResult}");
                break;
            }
        }
    }

    private void SendLog(EntityInteractionData interactionData, int entityHpResult)
    {
        IEntity sourceEntity = interactionData.SourceEntity;
        IEntity targetEntity = interactionData.TargetEntity;
        DefenceComponent targetDefenceComponent = targetEntity.GetEntityComponent<DefenceComponent>();
        HealthComponent targetHealthComponent = targetEntity.GetEntityComponent<HealthComponent>();
        switch (interactionData.InteractionResult)
        {
            case (InteractionResult.Hit):
            {
                Helper.Instance.AddLog(
                    $"{sourceEntity.Name} striked {targetEntity.Name} for {interactionData.TargetEntityDamageReceived}: " +
                    $"{targetDefenceComponent.Value} of {interactionData.SourceEntityDamageOutgoing} outgoing damage was blocked.\n");
                Helper.Instance.AddLog(
                    $"{targetEntity.Name} have {entityHpResult} health left.\n");
                break;
            }
            case (InteractionResult.Kill):
            {
                Helper.Instance.AddLog(
                    $"{sourceEntity.Name} striked {targetEntity.Name} for {interactionData.TargetEntityDamageReceived}: " +
                    $"{targetDefenceComponent.Value} of {interactionData.SourceEntityDamageOutgoing} outgoing damage was blocked.\n");
                Helper.Instance.AddLog(
                    $"{targetEntity.Name} have {entityHpResult} health left and died.\n");
                break;
            }
            case (InteractionResult.Dodge):
            {
                Helper.Instance.AddLog(
                    $"{sourceEntity.Name} tried to strike {targetEntity.Name}, but {targetEntity.Name} dodged the attack!\n");
                break;
            }
            case (InteractionResult.Heal):
            {
                Helper.Instance.AddLog(
                    $"{sourceEntity.Name} healed {targetEntity.Name} for {interactionData.SourceEntityHealOutgoing}: " +
                    $"{targetEntity.Name} now have {entityHpResult} health left.\n");
                break;
            }
            case (InteractionResult.StatusEffectTick):
            {
                Helper.Instance.AddLog(
                    $"{targetEntity.Name} was damaged by StatusEffect for {interactionData.TargetEntityDamageReceived}.\n");
                Helper.Instance.AddLog(
                    $"{targetEntity.Name} have {entityHpResult} health left.\n");
                break;
            
            }
        }
        
    }
}