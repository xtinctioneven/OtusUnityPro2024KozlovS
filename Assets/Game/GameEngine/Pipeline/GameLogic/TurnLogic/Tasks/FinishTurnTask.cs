using Game.Gameplay;
using UnityEngine;
using Zenject;

public sealed class FinishTurnTask : EventTask
{
    private readonly TurnOrderService _turnOrderService;
    private readonly EntityInteractionService _entityInteractionService;
    
    public FinishTurnTask(
        TurnOrderService turnOrderService,
        EntityInteractionService entityInteractionService
    )
    {
        _turnOrderService = turnOrderService;
        _entityInteractionService = entityInteractionService;
    }

    protected override void OnRun()
    {
        _turnOrderService.PrepareQueue();
        _entityInteractionService.Reset();
        Finish();
    }
}