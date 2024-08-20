using System.Collections.Generic;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class ConfigParser
    {
        ConfigParser(List<UserInfo> userInfos,
            List<PlayerLevel> playerLevels,
            List<CharacterInfo> characterInfos,
            PlayerConfigCollection playerConfigCollection)
        {
            for (int i = 0; i < playerConfigCollection._playerConfigs.Count; i++)
            {
                // userInfo = SetupUserInfo(playerConfig.UserConfig);
                // playerLevel = SetupPlayerLevel(playerConfig.LevelConfig);
                // characterInfo = SetupCharacterInfo(playerConfig.StatsConfigs);
                //
                // PlayerConfigModels playerConfigModels = _configParser.ParsePlayerConfig(_playerConfigCollection._playerConfigs[i]);
                PlayerConfig playerConfig = playerConfigCollection._playerConfigs[i]; 
                userInfos.Add(SetupUserInfo(playerConfig.UserConfig));
                playerLevels.Add(SetupPlayerLevel(playerConfig.LevelConfig)) ;
                characterInfos.Add(SetupCharacterInfo(playerConfig.StatsConfigs));
            }
        }
        
        // public PlayerConfigModels ParsePlayerConfig(PlayerConfig playerConfig)
        // {
        //     UserInfo userInfo = SetupUserInfo(playerConfig.UserConfig);
        //     PlayerLevel playerLevel = SetupPlayerLevel(playerConfig.LevelConfig);
        //     CharacterInfo characterInfo = SetupCharacterInfo(playerConfig.StatsConfigs);
        //     return new PlayerConfigModels
        //     {
        //         UserInfo = userInfo,
        //         PlayerLevel = playerLevel,
        //         CharacterInfo = characterInfo
        //     };
        // }
        
        private UserInfo SetupUserInfo(UserConfig userInfoConfig)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.ChangeIcon(userInfoConfig.Icon);
            userInfo.ChangeName(userInfoConfig.Name);
            userInfo.ChangeDescription(userInfoConfig.Description);
            return userInfo;
        }

        private PlayerLevel SetupPlayerLevel(LevelConfig playerLevelConfig)
        {
            PlayerLevel playerLevel = new PlayerLevel();
            while (playerLevel.CurrentLevel.Value < playerLevelConfig.Level)
            {
                playerLevel.AddExperience(playerLevel.RequiredExperience);
                playerLevel.LevelUp();
            }
            playerLevel.AddExperience(playerLevelConfig.Experience);
            return playerLevel;
        }

        private CharacterInfo SetupCharacterInfo(StatConfig[] characterStatsConfigs)
        {
            CharacterInfo characterInfo = new CharacterInfo();
            for (int i = 0; i < characterStatsConfigs.Length; i++)
            {
                var characterStat = new CharacterStat();
                characterStat.ChangeValue(characterStatsConfigs[i].Value);
                characterStat.ChangeModifier((characterStatsConfigs[i].LevelModifier));
                characterStat.ChangeName(characterStatsConfigs[i].Name);
                characterInfo.AddStat(characterStat);
            }

            return characterInfo;
        }
    }
    //
    // public struct PlayerConfigModels
    // {
    //     public UserInfo UserInfo;
    //     public PlayerLevel PlayerLevel;
    //     public CharacterInfo CharacterInfo;
    // }
}