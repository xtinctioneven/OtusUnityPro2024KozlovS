using System;
using UnityEngine;

public class ChestModel
{
    public event Action<TimeSpan> OnTimeToOpenChanged;
    public event Action<bool> OnChestReadyToOpen;
    public event Action OnCountdownRestart;
    public string Id { get; }
    public Sprite OpenChestImage { get; }
    public Sprite ClosedChestImage { get; }
    public TimeSpan CurrentTimeToOpen { get; private set; }
    public TimeSpan InitialTimeToOpen { get; private set; }
    public Reward[] Rewards { get; }
    public bool IsSafeToOpen { get; private set; }
    

    public ChestModel(string id, Sprite openChestImage, Sprite closedChestImage, TimeSpan timeToOpen, Reward[] rewards)
    {
        Id = id;
        OpenChestImage = openChestImage;
        ClosedChestImage = closedChestImage;
        CurrentTimeToOpen = timeToOpen;
        InitialTimeToOpen = timeToOpen;
        Rewards = rewards;
        IsSafeToOpen = false;
    }

    public void UpdateTimeToOpen(TimeSpan timeToOpen, bool isTimeReliable = false)
    {
        IsSafeToOpen = isTimeReliable;
        CurrentTimeToOpen = timeToOpen;
        if (timeToOpen <= TimeSpan.Zero)
        {
            OnChestReadyToOpen?.Invoke(IsSafeToOpen);
        }
        else
        {
            OnTimeToOpenChanged?.Invoke(timeToOpen);
        }
    }

    public void ReduceTimeToOpen(TimeSpan timeToSubtract, bool isTimeReliable = false)
    {
        UpdateTimeToOpen(CurrentTimeToOpen - timeToSubtract, isTimeReliable);
    }

    public void RestartCountdown()
    {
        OnCountdownRestart?.Invoke();
    }
}