using Game.Gameplay;
using UnityEngine;

public class StrikeTargetEffectHandler : BaseHandler<StrikeTargetEffect>
{
    public StrikeTargetEffectHandler(EventBus eventBus
        ) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(StrikeTargetEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        interactionData.SourceEntityDamageOutgoing =
            (int)Mathf.Floor(evt.AttackMultiplier * interactionData.SourceEntity.GetEntityComponent<AttackComponent>().Value);
        interactionData.InteractionResult = InteractionResult.Hit;
        foreach (var statusEffect in evt.StatusEffects)
        {
            if (statusEffect.EffectProbability >= Random.Range(0, 1))
            {
                interactionData.StatusEffectsApplyToTarget.Add(statusEffect.StatusEffect);
            }
        }
    }
}