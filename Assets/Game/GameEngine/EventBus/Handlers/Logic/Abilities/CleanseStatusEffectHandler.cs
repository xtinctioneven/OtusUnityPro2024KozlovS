using Game.Gameplay;
using Zenject;

public class CleanseStatusEffectHandler : BaseHandler<CleanseStatusEffect>
{
    public CleanseStatusEffectHandler(EventBus eventBus,
        DiContainer diContainer
    ) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(CleanseStatusEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        IEntity targetEntity = interactionData.TargetEntity;
        var statusEffects = targetEntity.GetEntityComponent<StatusEffectsComponent>().GetStatusesByType<IStickyStatusEffect>();
        int index = statusEffects.Count-1;
        while (evt.CleanseStatusesCount > 0 && index >= 0)
        {
            if (statusEffects[index].StatusEffectType == evt.CleanseStatusType
                && statusEffects[index].IsCleanseable)
            {
                EventBus.RaiseEvent(new RemoveStatusEffectEvent(statusEffects[index]));
                evt.CleanseStatusesCount--;
            }
            index--;
        }
    }
}