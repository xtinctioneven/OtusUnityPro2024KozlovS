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
        foreach (var statusEffectData in evt.StatusEffectsDataCollection)
        {
            if (statusEffectData.EffectProbability >= Random.Range(0, 1))
            {
                for (int i = 0; i < statusEffectData.StatusEffects.Length; i++)
                {
                    interactionData.StatusEffectsApplyToTarget.Add(statusEffectData.StatusEffects[i]);
                }
            }
        }
    }
}