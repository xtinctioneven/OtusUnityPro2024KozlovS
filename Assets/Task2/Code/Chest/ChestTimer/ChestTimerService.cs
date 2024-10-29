using System.Collections.Generic;
using Zenject;

public class ChestTimerService
{
    private List<ChestTimer> _chestTimers;
    private readonly ChestTimerFactory _chestTimerFactory;
    private readonly ServerTimeService _serverTimeService;

    [Inject]
    public ChestTimerService(ServerTimeService serverTimeService, ChestTimerFactory chestTimerFactory)
    {
        _chestTimerFactory = chestTimerFactory;
        _serverTimeService = serverTimeService;
    }
    
    public void Initialize(List<ChestModel> chests)
    {
        _chestTimers = new List<ChestTimer>();
        foreach (var chest in chests)
        {
            var chestTimer = _chestTimerFactory.Create(chest, _serverTimeService);
            _chestTimers.Add(chestTimer);
        }
    }

    public List<ChestTimer> GetChestTimers()
    {
        return _chestTimers;
    }
}