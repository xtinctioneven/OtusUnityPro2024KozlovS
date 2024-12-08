using UnityEngine;
using Zenject;

public sealed class FinishRoundTask : EventTask
{
    // private EntityInteractionService _entityInteractionService;
    private DiContainer _diContainer;
    private EventBus _eventBus;
    
    public FinishRoundTask(
        // EntityInteractionService entityInteractionService
        DiContainer diContainer,
        EventBus eventBus
    )
    {
        _diContainer = diContainer;
        _eventBus = eventBus;
        // _entityInteractionService = entityInteractionService;
    }

    protected override void OnRun()
    {
        // var turnOrderService = _diContainer.Resolve<TurnOrderService>();
        // var entityInteractionService = _diContainer.Resolve<EntityInteractionService>();
        var entitiesTracker = _diContainer.Resolve<EntityTrackerService>();
        var abilityService = _diContainer.Resolve<AbilityService>();
        var allEntities = entitiesTracker.GetAllEntities();
        for (int i = 0; i < allEntities.Count; i++)
        {
            abilityService.StatusEffectsTick(allEntities[i].GetEntityComponent<StatusEffectsComponent>());
        }
        // turnOrderService.PrepareQueue();
        // entityInteractionService.Reset();
        Finish();
    }
}