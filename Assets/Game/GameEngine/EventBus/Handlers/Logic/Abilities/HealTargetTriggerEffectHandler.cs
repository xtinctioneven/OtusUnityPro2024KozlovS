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
        foreach (var statusEffectData in evt.StatusEffectsDataCollection)
        {
            if (statusEffectData.EffectProbability >= Random.Range(0, 1))
            {
                for (int i = 0; i < statusEffectData.StatusEffectsApplyToTarget.Length; i++)
                {
                    //TODO: REPAIR
                    // interactionData.StatusEffectsApplyToTarget.Add(statusEffectData.StatusEffects[i]);
                }
            }
        }
        evt.TryUseCount(1);
    }
}