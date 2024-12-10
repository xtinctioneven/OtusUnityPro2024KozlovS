using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay
{
    [CreateAssetMenu(
        fileName = "CharacterConfig",
        menuName = "Battler Configs/New Character Config"
    )]
    public class CharacterConfig : ScriptableObject
    {
        [PreviewField(Alignment = ObjectFieldAlignment.Left), HideLabel] public Sprite CharacterIcon;
        public string CharacterName = "New Character";
        public Stat.StatData[] StatsData;
        public EntityView EntityViewPrefab;
        public GameObject CharacterVisualPrefab;
        [InlineEditor] public AbilityConfig[] Abilities = new AbilityConfig[5];
    }
}