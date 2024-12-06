using System.Collections.Generic;
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
            ApplyPassiveEffects(interactionData, sourceEntity.GetEntityComponent<AbilityComponent>());
            ApplyPassiveEffects(interactionData, targetEntity.GetEntityComponent<AbilityComponent>());
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

    public void ApplyPassiveEffects(EntityInteractionData interactionData, AbilityComponent abilityComponent)
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
    
    public void ApplyStatusEffects(IEntity entity, List<IStatusEffect> statusEffects, EntityInteractionData interactionData)
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
    
}