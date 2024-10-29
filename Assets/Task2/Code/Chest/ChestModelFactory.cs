using System;
using UnityEngine;

public class ChestModelFactory
{
    public ChestModel Create(ChestConfig chestConfig)
    {
        ChestConfig.SerializedTimeSpan serializedTimeSpan = chestConfig.TimeToOpenChest;
        TimeSpan seconds = TimeSpan.FromSeconds(serializedTimeSpan.Seconds);
        TimeSpan minutes = TimeSpan.FromMinutes(serializedTimeSpan.Minutes);
        TimeSpan hours = TimeSpan.FromHours(serializedTimeSpan.Hours);
        TimeSpan timeToOpen = seconds.Add(minutes).Add(hours);
        ChestModel chestModel = new ChestModel(chestConfig.Id, chestConfig.OpenChestImage, chestConfig.ClosedChestImage, 
            timeToOpen, chestConfig.Rewards);
        Debug.Log("ChestModelFactory: Created ChestModel with TimeSpan: " + timeToOpen);
        return chestModel;
    }
}