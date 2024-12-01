using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;

[CreateAssetMenu(
    fileName = "AbilityConfig",
    menuName = "Battler Configs/New Ability Config"
)]
public class AbilityConfig : ScriptableObject
{
    [SerializeReference] public List<IEffect> Abilities = new();

    public List<IEffect> CreateAbilities()
    {
        List<IEffect> abilities = new List<IEffect>();
        foreach (var ability in Abilities)
        {
            abilities.Add(ability.Clone());
        }
        return abilities;
    }
}