using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public interface IEffectApplyStatus
    {
        StatusEffectData[] StatusEffectsDataCollection { get; }

        public void ApplyStatusEffects(EntityInteractionData interactionData)
        {
            IEntity targetEntity = interactionData.TargetEntity;
            IEntity sourceEntity = interactionData.SourceEntity;
            foreach (var statusEffectData in StatusEffectsDataCollection)
            {
                if (statusEffectData.EffectProbability >= Random.Range(0, 1))
                {
                    for (int i = 0; i < statusEffectData.StatusEffectsApplyToTarget.Length; i++)
                    {
                        var statusEffect = statusEffectData.StatusEffectsApplyToTarget[i].StatusEffect.Clone();
                        statusEffect.AfflictedEntity = targetEntity;
                        interactionData.StatusEffectsApplyToTarget.Add(statusEffect);
                    }
                    for (int i = 0; i < statusEffectData.StatusEffectsApplyToSelf.Length; i++)
                    {
                        var statusEffect = statusEffectData.StatusEffectsApplyToSelf[i].StatusEffect.Clone();
                        statusEffect.AfflictedEntity = sourceEntity;
                        interactionData.StatusEffectsApplyToSource.Add(statusEffect);
                    }
                }
            }
        }
    }
}