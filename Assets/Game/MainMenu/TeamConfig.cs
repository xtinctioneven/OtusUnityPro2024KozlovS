using System.Collections;
using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;

[CreateAssetMenu(
    fileName = "TeamConfig",
    menuName = "Battler Configs/New Team Config"
)]
public class TeamConfig : ScriptableObject
{
    public List<CharacterConfig> characters = new List<CharacterConfig>();
}
