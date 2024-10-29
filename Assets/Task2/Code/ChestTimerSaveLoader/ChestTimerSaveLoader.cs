using System;

namespace SaveSystem
{
    [Serializable]
    public class ChestTimerSaveLoader : SaveLoader<ChestTimerService, ChestTimerServiceSaveData>
    {
        protected override ChestTimerServiceSaveData ConvertToData(ChestTimerService chestTimerService)
        {
            ChestTimerServiceSaveData chestTimerServiceSaveData = new ();
            var chestTimersList = chestTimerService.GetChestTimers();
            foreach (var chestTimer in chestTimersList)
            {
                ChestTimerServiceSaveData.ChestTimerSaveData timerSaveData = new()
                {
                    StartTime = chestTimer.CountdownStartTime
                };
                chestTimerServiceSaveData.ChestTimerSaveDataCollection.Add(chestTimer.Chest.Id, timerSaveData);
            }
            return chestTimerServiceSaveData;
        }

        protected override void SetupData(ChestTimerService chestTimerService, ChestTimerServiceSaveData chestTimerSaveData)
        {
            foreach (var keyValuePair in chestTimerSaveData.ChestTimerSaveDataCollection)
            {
                foreach (var chestTimer in chestTimerService.GetChestTimers())
                {
                    if (chestTimer.Chest.Id == keyValuePair.Key)
                    {
                        chestTimer.SetStartTime(keyValuePair.Value.StartTime);
                        break;
                    }
                }
            }
        }
    }
}