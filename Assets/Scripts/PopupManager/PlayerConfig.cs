using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lessons.Architecture.PM
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Data/New Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private UserConfig _userConfig;
        [SerializeField] private StatConfig[] _statsConfigs;
        [SerializeField] private LevelConfig _levelConfig;

        public UserConfig UserConfig => _userConfig;
        public StatConfig[] StatsConfigs => _statsConfigs;
        public LevelConfig LevelConfig => _levelConfig;
    }

    [Serializable]
    public struct UserConfig
    {
        public string Name;
        public string Description;
        public Sprite Icon;
    }

    [Serializable]
    public struct StatConfig
    {
        public string Name;
        public int Value;
        [FormerlySerializedAs("IncreaseOnLevelUp")] public int LevelModifier;
    }

    [Serializable]
    public struct LevelConfig
    {
        public int Level;
        public int Experience;
    }
}