﻿using System.Collections.Generic;
using Game.Gameplay;
using Zenject;

public class AbilityService
{
    private readonly DiContainer _diContainer;
    private readonly EventBus _eventBus;
    private TargetFinderService _targetFinderService;
    private EntityInteractionService _entityInteractionService;
    
    public AbilityService(DiContainer diContainer, EventBus eventBus)
    {
        _diContainer = diContainer;
        _eventBus = eventBus;
    }
    
    public void Initialize()
    {
        _targetFinderService = _diContainer.Resolve<TargetFinderService>();
        _entityInteractionService = _diContainer.Resolve<EntityInteractionService>();
    }

    public void UseAbility(IEntity sourceEntity, IEffect ability, IEntity targetEntity = null)
    {
        List<IEntity> targetsList = new List<IEntity>();
        if (targetEntity != null)
        {
            targetsList.Add(targetEntity);
        }
        else
        {
            targetsList = _targetFinderService.GetTargets(sourceEntity, ability as IEffectTarget);
        }

        for (int i = 0; i < targetsList.Count; i++)
        {
            targetEntity = targetsList[i];
            _entityInteractionService.SetTargetEntity(targetEntity);
            EntityInteractionData interactionData = _entityInteractionService.CreateCurrentInteractionData();
            if (interactionData.SourceEntity == null)
            {
                interactionData.SourceEntity = sourceEntity;
            }
            interactionData.SourceEffect = ability;
            ability.InteractionData = interactionData;
            _eventBus.RaiseEvent(ability);
            PassiveEffectsApply(interactionData, sourceEntity.GetEntityComponent<AbilityComponent>());
            PassiveEffectsApply(interactionData, targetEntity.GetEntityComponent<AbilityComponent>());
            _eventBus.RaiseEvent(new EntityInteractionEvent(ability.InteractionData));
            //Check for link abilities
            foreach (var statusEffect in ability.InteractionData.StatusEffectsApplyToTarget)
            {
                if (statusEffect is LinkStatusEffect linkStatus)
                {
                    LinkStatusType linkType = linkStatus.LinkStatus;
                    LinkEffectsTracker linkEffectsTracker = _diContainer.Resolve<LinkEffectsTracker>();
                    if (linkEffectsTracker.TryGetActiveLink(linkType,
                            sourceEntity.GetEntityComponent<Game.Gameplay.TeamComponent>().Value,
                            out IEffectLink linkAbility))
                    {
                        UseAbility(linkAbility.SourceEntity, linkAbility, targetEntity);
                    }

                    break;
                }
            }
        }
    }

    public void PassiveEffectsApply(EntityInteractionData interactionData, AbilityComponent abilityComponent)
    {
        var passiveAbilities = abilityComponent.GetAbilitiesByType<IEffectPassive>();
        foreach (var passiveAbility in passiveAbilities)
        {
            passiveAbility.InteractionData = interactionData;
            if (!passiveAbility.CanBeUsed)
            {
                continue;
            }
            _eventBus.RaiseEvent(passiveAbility);
        }
    }
    
    public void StatusEffectsApply(IEntity entity, List<IStatusEffect> statusEffects, EntityInteractionData interactionData)
    {
        StatusEffectsComponent statusEffectsComponent = entity.GetEntityComponent<StatusEffectsComponent>();
        LinkComponent linkComponent = entity.GetEntityComponent<LinkComponent>();
        foreach (IStatusEffect statusEffect in statusEffects)
        {
            statusEffect.InteractionData = interactionData;
            if (statusEffect is LinkStatusEffect linkStatusEffect)
            {
                linkComponent.ApplyStatus(linkStatusEffect.LinkStatus);
            }
            else
            {
                statusEffect.AfflictedEntity = entity;
                if (statusEffect is IConsumeStatusEffect)
                {
                    _eventBus.RaiseEvent(statusEffect);
                }
                else
                {
                    statusEffectsComponent.ApplyStatus(statusEffect);
                }
            }
        }
    }
    
    public void StatusEffectsTick(StatusEffectsComponent statusEffectsComponent)
    {
        foreach (IStatusEffect statusEffect in statusEffectsComponent.StatusEffects)
        {
            // statusEffect.InteractionData = interactionData;
            // statusEffect.AfflictedEntity = entity;
            statusEffect.InteractionData = _entityInteractionService.CreateEmptyInteractionData(
                targetEntity: statusEffect.AfflictedEntity);
            statusEffect.InteractionData.SourceEntity = statusEffect.SourceEntity ?? statusEffect.AfflictedEntity;
            _eventBus.RaiseEvent(statusEffect);
            _eventBus.RaiseEvent(new EntityInteractionEvent(statusEffect.InteractionData));
            if (statusEffect is IStickyStatusEffect { DurationLeft: <= 0 })
            {
                _eventBus.RaiseEvent(new RemoveStatusEffectEvent(statusEffect));
            }
        }
    }
    
}