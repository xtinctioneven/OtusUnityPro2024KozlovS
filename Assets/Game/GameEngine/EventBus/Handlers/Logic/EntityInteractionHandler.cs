using System;
using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;
using Zenject;

public class EntityInteractionHandler: BaseHandler<EntityInteractionEvent>
{
    private readonly AbilityService _abilityService;
    private readonly VisualPipeline _visualPipeline;
    public EntityInteractionHandler(EventBus eventBus, AbilityService abilityService,VisualPipeline visualPipeline) : base(eventBus)
    {
        _abilityService = abilityService;
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(EntityInteractionEvent evt)
    {
        // _abilityService = _diContainer.Resolve<AbilityService>();
        EntityInteractionData interactionData = evt.EntityInteractionData;
        //Resolve interaction data
        ResolveInteractionData(interactionData);
        //Apply results
        //Source entity
        IEntity sourceEntity = interactionData.SourceEntity;
        _abilityService.StatusEffectsApply(sourceEntity, interactionData.StatusEffectsApplyToSource, interactionData);
        HealthComponent healthComponent = sourceEntity.GetEntityComponent<HealthComponent>();
        int sourceEntityHpResult = healthComponent.Value - interactionData.SourceEntityDamageReceived + interactionData.SourceEntityHealReceived;
        sourceEntityHpResult = Math.Clamp(sourceEntityHpResult, HealthComponent.MIN_LIFE, healthComponent.MaxValue);
        //Target entity
        IEntity targetEntity = interactionData.TargetEntity;
        _abilityService.StatusEffectsApply(targetEntity, interactionData.StatusEffectsApplyToTarget, interactionData);
        healthComponent = targetEntity.GetEntityComponent<HealthComponent>();
        int targetEntityHpResult = healthComponent.Value - interactionData.TargetEntityDamageReceived + interactionData.TargetEntityHealReceived;
        targetEntityHpResult = Math.Clamp(targetEntityHpResult, HealthComponent.MIN_LIFE, healthComponent.MaxValue);
        if (targetEntityHpResult == 0)
        {
            interactionData.InteractionResult = InteractionResult.Kill;
        }
        _abilityService.PassiveEffectsApply(interactionData, sourceEntity.GetEntityComponent<AbilityComponent>());
        _abilityService.PassiveEffectsApply(interactionData, targetEntity.GetEntityComponent<AbilityComponent>());
        if (interactionData.SourceEffect.AbilityVisualData != null)
        {
            _visualPipeline.AddTask(new AnimateAbilityVisualTask(interactionData));
        }
        EventBus.RaiseEvent(new UpdateStatsEvent(sourceEntity, sourceEntityHpResult));
        EventBus.RaiseEvent(new UpdateStatsEvent(targetEntity, targetEntityHpResult));
        SendLog(interactionData, targetEntityHpResult);
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
                Helper.Instance.AddLog($"Interaction result is not set for ability: {interactionData.SourceEffect}\n");
                break;
            }
            case (InteractionResult.StatusEffectTick):
            {
                defenceComponent = interactionData.TargetEntity.GetEntityComponent<DefenceComponent>();
                interactionData.TargetEntityDamageReceived = Math.Max(interactionData.SourceEntityDamageOutgoing - defenceComponent.Value, 0);
                
                break;
            }
            case (InteractionResult.Buff):
            {
                
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
                    $"{sourceEntity.Name} damaged {targetEntity.Name} for {interactionData.TargetEntityDamageReceived}: " +
                    $"{targetDefenceComponent.Value} of {interactionData.SourceEntityDamageOutgoing} outgoing damage was blocked.\n");
                Helper.Instance.AddLog(
                    $"{targetEntity.Name} have {entityHpResult} health left.\n");
                break;
            }
            case (InteractionResult.Kill):
            {
                Helper.Instance.AddLog(
                    $"{sourceEntity.Name} damaged {targetEntity.Name} for {interactionData.TargetEntityDamageReceived}: " +
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
            case (InteractionResult.Buff):
            {
                Helper.Instance.AddLog(
                    $"{sourceEntity.Name} used buff action on {targetEntity.Name}.\n");
                break;
            
            }
        }
        
    }
}