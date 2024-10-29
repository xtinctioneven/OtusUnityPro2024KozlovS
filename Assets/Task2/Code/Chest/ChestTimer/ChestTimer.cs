using System;
using System.Threading;

public class ChestTimer
{
    public ChestModel Chest { get; }
    public DateTime CountdownStartTime { get; private set; }
    private bool _isTimeReliable; 
    private readonly ServerTimeService _serverTimeService;

    public ChestTimer(ChestModel chest, ServerTimeService serverTimeService)
    {
        Chest = chest;
        _isTimeReliable = false;
        _serverTimeService = serverTimeService;
        RestartTimer();
        Chest.OnCountdownRestart += RestartTimer;
    }

    public void SetStartTime(DateTime newStartTime)
    {
        CountdownStartTime = newStartTime;
        RecalculateTimeToOpenChest();
    }

    private void RestartTimer()
    {
        _serverTimeService.OnTick += ServerTimeServiceOnTick;
        _serverTimeService.OnSynchronize += ServerTimeServiceOnSynchronize;
        Chest.OnChestReadyToOpen += StopTimer;
        SetStartTime(_serverTimeService.GetCurrentTime());
    }

    private void StopTimer(bool isSafeToOpen)
    {
        if (isSafeToOpen)
        {
            _serverTimeService.OnSynchronize -= ServerTimeServiceOnSynchronize;
        }
        _serverTimeService.OnTick -= ServerTimeServiceOnTick;
        Chest.OnChestReadyToOpen -= StopTimer;
    }

    private void ServerTimeServiceOnTick(TimeSpan timePassed, bool isTimeReliable)
    {
        _isTimeReliable = isTimeReliable;
        Chest.ReduceTimeToOpen(timePassed, _isTimeReliable);
    }

    private void ServerTimeServiceOnSynchronize(bool isProblemDetected)
    {
        _isTimeReliable = true;
        if (isProblemDetected || !Chest.IsSafeToOpen)
        {
            RecalculateTimeToOpenChest();
        }
    }

    private void RecalculateTimeToOpenChest()
    {
        var currentTime = _serverTimeService.GetCurrentTime();
        TimeSpan newTimeToOpen = CountdownStartTime + Chest.InitialTimeToOpen - currentTime;
        Chest.UpdateTimeToOpen(newTimeToOpen, _isTimeReliable);
    }
}