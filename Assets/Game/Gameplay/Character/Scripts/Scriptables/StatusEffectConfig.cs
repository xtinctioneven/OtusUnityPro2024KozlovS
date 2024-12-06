using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;

[CreateAssetMenu(
    fileName = "StatusEffectConfig",
    menuName = "Battler Configs/New StatusEffect Config"
)]
public class StatusEffectConfig : ScriptableObject
{
    [SerializeReference] public IStatusEffect StatusEffect;
}