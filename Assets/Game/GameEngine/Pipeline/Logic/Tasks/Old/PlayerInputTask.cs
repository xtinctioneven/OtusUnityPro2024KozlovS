using System;
using UI;
using UnityEngine;
using Zenject;

public class PlayerInputTask : EventTask
{
    private readonly TurnOrderService _turnOrderService;
    private readonly UIService _uiService;
    private HeroListView _opponentListView;
    private readonly EventBus _eventBus;

    public PlayerInputTask(
        TurnOrderService turnOrderService,
        UIService uiService,
        EventBus eventBus)
    {
        _eventBus = eventBus;
        _uiService = uiService;
        _turnOrderService = turnOrderService;
    }

    protected override void OnRun()
    {
        // Debug.Log("PlayerInputTask.OnRun");
        // string heroName = _turnOrderService.GetActiveHero().GetComponent<HeroModel>().HeroConfig.HeroName;
        // Debug.Log($"{heroName} ходит");
        // Team opponentTeam = _turnOrderService.GetActiveOpponentTeam();
        // _opponentListView = opponentTeam == Team.BlueTeam ? _uiService.GetBluePlayer() : _uiService.GetRedPlayer();
        // _opponentListView.OnHeroClicked += OnHeroClicked;
    }

    private void OnHeroClicked(HeroView clickedHeroView)
    {
        _opponentListView.OnHeroClicked -= OnHeroClicked;
        _eventBus.RaiseEvent(new TargetHeroClickedEvent(clickedHeroView));
        Finish();
    }
}