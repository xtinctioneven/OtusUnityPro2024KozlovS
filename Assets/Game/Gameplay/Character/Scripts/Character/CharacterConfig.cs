using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(
        fileName = "CharacterConfig",
        menuName = "Battler Configs/New Character Config"
    )]
    public class CharacterConfig : ScriptableObject
    {
        public string CharacterName = "New Character";
        public Stat.StatData[] StatsData;
        [InlineEditor] public AbilityConfig[] Abilities = new AbilityConfig[5];
    }
}