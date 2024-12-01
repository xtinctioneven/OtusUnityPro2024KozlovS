using System;
using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine.Serialization;

[Serializable]
public class AbilityComponent
{
    private List<IEffect> _abilities = new List<IEffect>();
    public AbilityConfig[] AbilityConfigs;

    public AbilityComponent(AbilityConfig[] configs)
    {
        AbilityConfigs = configs;
    }

    public void Install(IEntity entity)
    {
        for (int i = 0; i < AbilityConfigs.Length; i++)
        {
            _abilities.AddRange(AbilityConfigs[i].CreateAbilities());
        }
        foreach (var ability in _abilities)
        {
            ability.Enable();
            ability.SourceEntity = entity;
        }
    } 

    public List<IEffect> GetAllAbilities()
    {
        return _abilities;
    }

    public bool TryRemoveAbility(IEffect ability)
    {
        if (!_abilities.Contains(ability)) return false;
        _abilities.Remove(ability);
        return true;
    }

    public List<TEffectType> GetAbilitiesByType<TEffectType>()
    {
        List<TEffectType> abilities = new List<TEffectType>();
        foreach (var ability in _abilities)
        {
            if (ability is TEffectType)
            {
                abilities.Add((TEffectType)ability);
            }
        }
        return abilities;
    }

    public void ResetCounts()
    {
        foreach (var ability in GetAbilitiesByType<IEffectUseCounts>())
        {
            ability.ResetCounts();
        }
    }
}