using System.Collections.Generic;
using Zenject;

public class TurnOrderServiceOld: IInitializable
{
    private EventBus _eventBus;
    private readonly HeroTrackerService _heroTrackerService;
    private readonly Queue<HeroEntity> _redHeroesQueue = new();
    private readonly Queue<HeroEntity> _blueHeroesQueue = new();
    //private Team _nextPlayerTeam = Team.RedTeam;
    private HeroEntity _activeHero;
    
    public TurnOrderServiceOld(
        EventBus eventBus,
        HeroTrackerService heroTrackerService
        )
    {
        _eventBus = eventBus;
        _heroTrackerService = heroTrackerService;
    }

    public void Initialize()
    {
        EnqueueTeam(_redHeroesQueue, _heroTrackerService.GetRedTeam());
        EnqueueTeam(_blueHeroesQueue, _heroTrackerService.GetBlueTeam());
    }

    private void EnqueueTeam(Queue<HeroEntity> heroesQueue, IReadOnlyList<HeroEntity> heroList)
    {
        foreach (HeroEntity hero in heroList)
        {
            heroesQueue.Enqueue(hero);
        }
    }

    // public HeroEntity ActivateNextHero()
    // {
        // if (_nextPlayerTeam == Team.RedTeam)
        // {
        //     if (_redHeroesQueue.Count == 0)
        //     {
        //         EnqueueTeam(_redHeroesQueue, _heroTrackerService.GetRedTeam());
        //     }
        //     while (_redHeroesQueue.Peek() == null)
        //     {
        //         _redHeroesQueue.Dequeue();
        //         if (_redHeroesQueue.Count == 0)
        //         {
        //             EnqueueTeam(_redHeroesQueue, _heroTrackerService.GetRedTeam());
        //         }
        //     }
        //     _activeHero = _redHeroesQueue.Dequeue();
        //     _nextPlayerTeam = Team.BlueTeam;
        // }
        // else
        // {
        //     if (_blueHeroesQueue.Count == 0)
        //     {
        //         EnqueueTeam(_blueHeroesQueue, _heroTrackerService.GetBlueTeam());
        //     }
        //     while (_blueHeroesQueue.Peek() == null)
        //     {
        //         _blueHeroesQueue.Dequeue();
        //         if (_blueHeroesQueue.Count == 0)
        //         {
        //             EnqueueTeam(_blueHeroesQueue, _heroTrackerService.GetBlueTeam());
        //         }
        //     }
        //     _activeHero = _blueHeroesQueue.Dequeue();
        //     _nextPlayerTeam = Team.RedTeam;
        // }
        // return _activeHero;
    // }
    
    public void Clear()
    {
        _redHeroesQueue.Clear();
        _blueHeroesQueue.Clear();
        _activeHero = null;
        // _nextPlayerTeam = Team.RedTeam;
    }

    public HeroEntity GetActiveHero()
    {
        return _activeHero;
    }

    // public Team GetActivePlayerTeam()
    // {
    //     return _nextPlayerTeam == Team.RedTeam ? Team.BlueTeam : Team.RedTeam;
    // }
    //
    // public Team GetActiveOpponentTeam()
    // {
    //     return _nextPlayerTeam;
    // }
}