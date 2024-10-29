using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject.ReflectionBaking.Mono.Cecil;

namespace SaveSystem
{
    [Serializable]
    public class GameSessionSaveLoader : SaveLoader<GameSessionService, GameSessionSaveData>
    {
        protected override GameSessionSaveData ConvertToData(GameSessionService gameSessionService)
        {
            GameSessionSaveData gameSessionSaveData = new GameSessionSaveData();
            gameSessionSaveData.SessionTimeDataCollection = gameSessionService.GetAllSessionsData();
            return gameSessionSaveData;
        }

        protected override void SetupData(GameSessionService gameSessionService, GameSessionSaveData gameSessionSaveData)
        {
            var gameSessionsData = gameSessionSaveData.SessionTimeDataCollection;
            gameSessionService.SetupData(gameSessionsData);
        }
    }
}