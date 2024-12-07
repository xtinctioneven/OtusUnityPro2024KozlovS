using System;
using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine.Serialization;

[Serializable]
public class StatusEffectsComponent
{
    public List<IStatusEffect> StatusEffects { get; private set; } = new List<IStatusEffect>();
    private IEntity _afflictedEntity;
    
    public StatusEffectsComponent(IEntity afflictedEntity)
    {
        _afflictedEntity = afflictedEntity;
    }

    public void ApplyStatus(IStatusEffect effect)
    {
        if (!StatusEffects.Contains(effect))
        {
            StatusEffects.Add(effect);
        }
        else
        {
            //TODO: Replace existing with new!
        }
    }

    public bool TryRemoveStatus(IStatusEffect effect)
    {
        if (!StatusEffects.Contains(effect))
        {
            return false;
        }
        StatusEffects.Remove(effect);
        return true;
    }
    
    public List<TStatusType> GetStatusesByType<TStatusType>()
    {
        List<TStatusType> statuses = new List<TStatusType>();
        foreach (var status in StatusEffects)
        {
            if (status is TStatusType)
            {
                statuses.Add((TStatusType)status);
            }
        }
        return statuses;
    }
}