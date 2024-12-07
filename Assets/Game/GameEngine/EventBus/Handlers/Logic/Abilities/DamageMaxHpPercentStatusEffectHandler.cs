using Game.Gameplay;
using UnityEngine;
using Zenject;

public class DamageMaxHpPercentStatusEffectHandler : BaseHandler<DamageMaxHpPercentStatusEffect>
{
    public DamageMaxHpPercentStatusEffectHandler(EventBus eventBus,
        DiContainer diContainer
    ) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(DamageMaxHpPercentStatusEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        int damageAmount =
            (int)Mathf.Floor(evt.AfflictedEntity.GetEntityComponent<HealthComponent>().MaxValue * evt.DamagePerTick);
        interactionData.SourceEntityDamageOutgoing = damageAmount;
        interactionData.InteractionResult = InteractionResult.StatusEffectTick;
        evt.Tick();
    }
}