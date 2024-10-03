using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

[Serializable]
public class AbilityComponent
{
    private List<IEffect> _abilities = new List<IEffect>();
    public AbilityConfig Ability;

    public AbilityComponent(AbilityConfig config)
    {
        Ability = config;
    }

    public void Install()
    {
        foreach (var ability in Ability.Abilities)
        {
            _abilities.Add(ability);
        }
    } 

    public List<IEffect> GetAbilities()
    {
        return _abilities;
    }

    public bool TryRemoveAbility(IEffect ability)
    {
        if (!_abilities.Contains(ability)) return false;
        _abilities.Remove(ability);
        return true;
    }
}