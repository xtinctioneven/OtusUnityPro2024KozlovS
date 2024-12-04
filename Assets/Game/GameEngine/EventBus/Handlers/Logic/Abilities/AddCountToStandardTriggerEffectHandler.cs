using Game.Gameplay;
using Zenject;

public class AddCountToStandardTriggerEffectHandler : BaseHandler<AddCountToStandardTriggerEffect>
{
    private DiContainer _diContainer;
    
    public AddCountToStandardTriggerEffectHandler(EventBus eventBus,
        DiContainer diDiContainer
    ) : base(eventBus)
    {
        _diContainer = diDiContainer;
    }

    protected override void OnHandleEvent(AddCountToStandardTriggerEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        var standardAbilities = interactionData.TargetEntity.
            GetEntityComponent<AbilityComponent>().GetAbilitiesByType<IEffectStandard>();
        foreach (var ability in standardAbilities)
        {
            ability.AddCount(evt.CountsToAdd);
        }
    }
}