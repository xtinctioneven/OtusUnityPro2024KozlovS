using Game.Gameplay;
using UnityEngine;
using Zenject;

public class HealTargetTriggerEffectHandler : BaseHandler<HealTargetTriggerEffect>
{
    public HealTargetTriggerEffectHandler(EventBus eventBus,
        DiContainer diDiContainer
    ) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(HealTargetTriggerEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        interactionData.SourceEntityHealOutgoing =
            (int)Mathf.Floor(evt.AttackMultiplier * interactionData.SourceEntity.GetEntityComponent<AttackComponent>().Value);
        interactionData.InteractionResult = InteractionResult.Heal;
        foreach (var statusEffect in evt.StatusEffects)
        {
            if (statusEffect.EffectProbability >= Random.Range(0, 1))
            {
                interactionData.StatusEffectsApplyToTarget.Add(statusEffect.StatusEffect);
            }
        }
    }
}