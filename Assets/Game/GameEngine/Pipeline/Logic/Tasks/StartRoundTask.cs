using System.Collections.Generic;
using Game.Gameplay;
using Zenject;

public class StartRoundTask : EventTask
{
    private readonly EventBus _eventBus;
    private DiContainer _diContainer;
    private AbilityService _abilityService;
    
    public StartRoundTask(
        DiContainer diContainer,
        EventBus eventBus)
    {
        _eventBus = eventBus;
        _diContainer = diContainer;
    }

    protected override void OnRun()
    {
        // _activeHeroEntity = _turnOrderService.ActivateNextHero();
        //Reset UseCounts
        _abilityService = _diContainer.Resolve<AbilityService>();
        var entityTracker = _diContainer.Resolve<EntityTrackerService>();
        var allEntities = entityTracker.GetAllEntities();
        foreach (var entity in allEntities)
        {
            entity.GetEntityComponent<AbilityComponent>().ResetCounts();
            
            List<IEffectTrigger> effectTriggers = entity.GetEntityComponent<AbilityComponent>().GetAbilitiesByType<IEffectTrigger>();
            //Use Trigger abilites
            for (int i = 0; i < effectTriggers.Count; i++)
            {
                if (effectTriggers[i].TriggerReason != TriggerReason.RoundStart || !effectTriggers[i].CanBeUsed)
                {
                    continue;
                }
                
                _abilityService.UseAbility(entity, effectTriggers[i]);
            }
        }
        Finish();
    }
}