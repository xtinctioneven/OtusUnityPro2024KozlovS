using Game.Gameplay;
using UnityEngine;
using Zenject;

public sealed class FinishTurnTask : EventTask
{
    // private EntityInteractionService _entityInteractionService;
    private DiContainer _diContainer;
    
    public FinishTurnTask(
        // EntityInteractionService entityInteractionService
        DiContainer diContainer
    )
    {
        _diContainer = diContainer;
        // _entityInteractionService = entityInteractionService;
    }

    protected override void OnRun()
    {
        var turnOrderService = _diContainer.Resolve<TurnOrderService>();
        var entityInteractionService = _diContainer.Resolve<EntityInteractionService>(); 
        var interactionData = entityInteractionService.GetCurrentInteractionData();
        if (interactionData != null)
        {
            var target = interactionData.TargetEntity;
            Helper.Instance.Log += $"{target.Name} have {target.GetEntityComponent<HealthComponent>().Value} health left.\n";
        }
        Debug.Log("Run finish round task");
        turnOrderService.PrepareQueue();
        entityInteractionService.Reset();
        Finish();
    }
}