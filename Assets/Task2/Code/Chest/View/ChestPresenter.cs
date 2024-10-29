using System;
using UnityEngine;

public class ChestPresenter
{
    public event Action<string> OnTimeToOpenChanged;
    public event Action<bool> OnChestReadyToOpen;
    public event Action OnChestRestart;
    public Sprite OpenChestImage { get; }
    public Sprite ClosedChestImage { get; }
    public Reward[] Rewards { get; }
    public string TimeToOpenChest => _timeToOpenChest.ToString(@"hh\:mm\:ss");
    private TimeSpan _timeToOpenChest;
    public ChestModel ChestModel { get; }

    public ChestPresenter(ChestModel chestModel)
    {
        ChestModel = chestModel;
        OpenChestImage = ChestModel.OpenChestImage;
        ClosedChestImage = ChestModel.ClosedChestImage;
        _timeToOpenChest = ChestModel.CurrentTimeToOpen;
        Rewards = ChestModel.Rewards;
        
        ChestModel.OnTimeToOpenChanged += ChestModelOnTimeToOpenChanged;
        ChestModel.OnChestReadyToOpen += ChestModelOnChestReadyToOpen;
        ChestModel.OnCountdownRestart += ChestModelOnCountdownRestart;
    }

    private void ChestModelOnCountdownRestart()
    {
        ChestModel.OnTimeToOpenChanged += ChestModelOnTimeToOpenChanged;
        OnChestRestart?.Invoke();
    }

    private void ChestModelOnTimeToOpenChanged(TimeSpan newTime)
    {
        _timeToOpenChest = newTime;
        OnTimeToOpenChanged?.Invoke(TimeToOpenChest);
    }

    private void ChestModelOnChestReadyToOpen(bool isSafeToOpen)
    {
        ChestModel.OnTimeToOpenChanged -= ChestModelOnTimeToOpenChanged;
        OnChestReadyToOpen?.Invoke(isSafeToOpen);
    }
}