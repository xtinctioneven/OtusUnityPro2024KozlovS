using System.Collections.Generic;
using Game.Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(
    fileName = "AbilityConfig",
    menuName = "Battler Configs/New Ability Config"
)]
public class AbilityConfig : ScriptableObject
{
    [PreviewField(ObjectFieldAlignment.Left), HideLabel] public Sprite Icon;
    public string Name;
    public string Description;
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