using Game.Gameplay;
using UnityEngine;

public class StrikeTargetLinkEffecttHandler : BaseHandler<StrikeTargetLinkEffect>
{
    public StrikeTargetLinkEffecttHandler(EventBus eventBus
    ) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(StrikeTargetLinkEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        interactionData.SourceEntityDamageOutgoing =
            (int)Mathf.Floor(evt.AttackMultiplier * interactionData.SourceEntity.GetEntityComponent<AttackComponent>().Value);
        interactionData.InteractionResult = InteractionResult.Hit;
        (evt as IEffectApplyStatus).ApplyStatusEffects(interactionData.SourceEntity, interactionData);
        evt.TryUseCount();
    }
}