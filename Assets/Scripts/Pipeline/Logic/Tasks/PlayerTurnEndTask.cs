using System.Linq;
using UnityEngine;
using Zenject.SpaceFighter;

public class PlayerTurnEndTask : EventTask
{
    private HeroEntity _activeHero;
    private readonly EventBus _eventBus;
    private readonly TurnOrderService _turnOrderService;
    private readonly HeroTrackerService _heroTrackerService;
    
    public PlayerTurnEndTask(
        EventBus eventBus,
        TurnOrderService turnOrderService,
        HeroTrackerService heroTrackerService
        )
    {
        _turnOrderService = turnOrderService;
        _heroTrackerService = heroTrackerService;
        _eventBus = eventBus;
    }

    protected override void OnRun()
    {
        Debug.Log("PlayerTurnEndTask.OnRun");
        _activeHero = _turnOrderService.GetActiveHero();
        HeroEntity[] activePlayerTeamHeroes =
            _heroTrackerService.GetHeroesFromTeam(_activeHero.GetHeroComponent<TeamComponent>().Value).ToArray();
        for (int i = 0; i < activePlayerTeamHeroes.Length; i++)
        {
            _eventBus.RaiseEvent(new ActivateAbilitiesEvent(TurnPhase.PlayerTurnEnd, activePlayerTeamHeroes[i]));
        }
        _eventBus.RaiseEvent(new DeactivateHeroEvent(_activeHero));
        Finish();
    }
}