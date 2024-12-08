using Game.Gameplay;
using UnityEngine;
using Zenject;

public class HealTargetTriggerEffectHandler : BaseHandler<HealTargetTriggerEffect>
{
    public HealTargetTriggerEffectHandler(EventBus eventBus,
        DiContainer diContainer
    ) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(HealTargetTriggerEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        interactionData.SourceEntityHealOutgoing =
            (int)Mathf.Floor(evt.AttackMultiplier * interactionData.SourceEntity.GetEntityComponent<AttackComponent>().Value);
        interactionData.InteractionResult = InteractionResult.Heal;
        (evt as IEffectApplyStatus).ApplyStatusEffects(interactionData.SourceEntity, interactionData);
        evt.TryUseCount(1);
    }
}