using Game.Gameplay;

public class RemoveStatusEffectEventHandler: BaseHandler<RemoveStatusEffectEvent>
{
    public RemoveStatusEffectEventHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(RemoveStatusEffectEvent evt)
    {
        IEntity afflictedEntity = evt.StatusEffect.AfflictedEntity;
        afflictedEntity.GetEntityComponent<StatusEffectsComponent>().TryRemoveStatus(evt.StatusEffect);
        
        Helper.Instance.AddLog($"Removed status effect {(evt.StatusEffect as IStickyStatusEffect)?.Name} from afflicted entity {afflictedEntity.Name}.");
    }
}