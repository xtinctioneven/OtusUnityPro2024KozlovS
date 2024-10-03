using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "AbilityConfig",
    menuName = "Battler Configs/New Ability Config"
)]
public class AbilityConfig : ScriptableObject
{
    [SerializeReference] public List<IEffect> Abilities = new();
}