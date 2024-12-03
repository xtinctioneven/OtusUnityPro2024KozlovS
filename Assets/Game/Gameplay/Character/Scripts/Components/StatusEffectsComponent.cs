using System;
using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine.Serialization;

[Serializable]
public class StatusEffectsComponent
{
    public List<IStatusEffect> StatusEffects { get; private set; } = new List<IStatusEffect>();

    public StatusEffectsComponent()
    {
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

    public void Tick(int tickTimes = 1)
    {
        for (int i = 0; i < StatusEffects.Count; i++)
        {
            //TODO:!!!
            // if (StatusEffects[i] is StatusEffect statusEffect)
            // {
            //     statusEffect.
            // }
        }
    }
}