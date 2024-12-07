using Game.Gameplay;
using Zenject;

public class AddCountToStandardTriggerEffectHandler : BaseHandler<AddCountToStandardTriggerEffect>
{
    private DiContainer _diContainer;
    
    public AddCountToStandardTriggerEffectHandler(EventBus eventBus,
        DiContainer diContainer
    ) : base(eventBus)
    {
        _diContainer = diContainer;
    }

    protected override void OnHandleEvent(AddCountToStandardTriggerEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        var standardAbilities = interactionData.TargetEntity.
            GetEntityComponent<AbilityComponent>().GetAbilitiesByType<IEffectStandard>();
        interactionData.InteractionResult = InteractionResult.Buff;
        foreach (var ability in standardAbilities)
        {
            ability.AddCount(evt.CountsToAdd);
        }
    }
}