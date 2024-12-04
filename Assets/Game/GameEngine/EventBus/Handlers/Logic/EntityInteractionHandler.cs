using System;
using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;

public class EntityInteractionHandler: BaseHandler<EntityInteractionEvent>
{
    public EntityInteractionHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(EntityInteractionEvent evt)
    {
        EntityInteractionData interactionData = evt.EntityInteractionData;
        //Resolve interaction data
        ResolveInteractionData(interactionData);
        //Apply results
        //Source entity
        IEntity sourceEntity = interactionData.SourceEntity;
        HealthComponent healthComponent = sourceEntity.GetEntityComponent<HealthComponent>();
        int entityHpResult = healthComponent.Value - interactionData.SourceEntityDamageReceived + interactionData.SourceEntityHealReceived;
        entityHpResult = Math.Clamp(entityHpResult, HealthComponent.MIN_LIFE, healthComponent.MaxValue);
        ApplyStatusEffects(sourceEntity, interactionData.StatusEffectsApplyToSource);
        EventBus.RaiseEvent(new UpdateStatsEvent(sourceEntity, entityHpResult));
        //Target entity
        IEntity targetEntity = interactionData.TargetEntity;
        healthComponent = targetEntity.GetEntityComponent<HealthComponent>();
        entityHpResult = healthComponent.Value - interactionData.TargetEntityDamageReceived + interactionData.TargetEntityHealReceived;
        entityHpResult = Math.Clamp(entityHpResult, HealthComponent.MIN_LIFE, healthComponent.MaxValue);
        ApplyStatusEffects(targetEntity, interactionData.StatusEffectsApplyToTarget);
        if (entityHpResult == 0)
        {
            interactionData.InteractionResult = InteractionResult.Kill;
        }
        EventBus.RaiseEvent(new UpdateStatsEvent(targetEntity, entityHpResult));
        ApplyPassiveEffects(sourceEntity, interactionData, sourceEntity.GetEntityComponent<AbilityComponent>());
        ApplyPassiveEffects(targetEntity, interactionData, targetEntity.GetEntityComponent<AbilityComponent>());
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

            default:
            {
                Debug.LogError($"Unhandled interaction result: {interactionData.InteractionResult}");
                break;
            }
        }
    }

    private void ApplyStatusEffects(IEntity entity, List<IStatusEffect> statusEffects)
    {
        //TODO: Extract to event + handler?
        StatusEffectsComponent statusEffectsComponent = entity.GetEntityComponent<StatusEffectsComponent>();
        LinkComponent linkComponent = entity.GetEntityComponent<LinkComponent>();
        foreach (IStatusEffect statusEffect in statusEffects)
        {
            if (statusEffect is LinkStatusEffect linkStatusEffect)
            {
                linkComponent.ApplyStatus(linkStatusEffect.LinkStatus);
            }
            else
            {
                statusEffectsComponent.ApplyStatus(statusEffect);
            }
        }
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
            EventBus.RaiseEvent(passiveAbility);
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
        }
        
    }
}