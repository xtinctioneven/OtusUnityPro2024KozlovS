using System.Collections.Generic;
using Game.Gameplay;
using Zenject;

public class TriggerEffectsTracker
{
    private EventBus _eventBus;
    private DiContainer _diContainer;
    private EntityTrackerService _entityTrackerService;
    private Dictionary<TriggerReason, List<IEffectTrigger>> _triggerAbilitiesCollection;
    // private List<IEffectTrigger> _battleStartAbilities = new List<IEffectTrigger>();
    // private List<IEffectTrigger> _roundStartAbilities = new List<IEffectTrigger>();
    // private List<IEffectTrigger> _turnStartAbilities = new List<IEffectTrigger>();
    // private List<IEffectTrigger> _beforeAnyActionAbilities = new List<IEffectTrigger>();
    
    public TriggerEffectsTracker(
        EventBus eventBus,
        DiContainer diContainer
    )
    {
        _eventBus = eventBus;
        _diContainer = diContainer;
    }

    public void Initialize()
    {
        _triggerAbilitiesCollection = new Dictionary<TriggerReason, List<IEffectTrigger>>();
        _entityTrackerService = _diContainer.Resolve<EntityTrackerService>();
        _entityTrackerService.OnEntityTracked += TrackEntity;
        _entityTrackerService.OnEntityUntracked += UntrackEntity;
        // var allEntities = _entityTrackerService.GetAllEntities();
        // for (int i = 0; i < allEntities.Count; i++)
        // {
        //     TrackEntity(allEntities[i]);
        // }
    }

    public bool GetTriggers(TriggerReason reason, out List<IEffectTrigger> triggers)
    {
        triggers = new List<IEffectTrigger>();
        if (!_triggerAbilitiesCollection.ContainsKey(reason))
        {
            return false;
        }
        var possibleTriggers = _triggerAbilitiesCollection[reason];
        for (int i = 0; i < possibleTriggers.Count; i++)
        {
            if (!possibleTriggers[i].CanBeUsed)
            {
                continue;
            }
            triggers.Add(possibleTriggers[i]);
        }
        return true;
    }

    private void TrackEntity(IEntity entity)
    {
        var abilityComponent = entity.GetEntityComponent<AbilityComponent>();
        foreach (var triggerAbility in abilityComponent.GetAbilitiesByType<IEffectTrigger>())
        {
            if (!_triggerAbilitiesCollection.ContainsKey(triggerAbility.TriggerReason))
            {
                _triggerAbilitiesCollection.Add(triggerAbility.TriggerReason, new List<IEffectTrigger>());
            }
            _triggerAbilitiesCollection[triggerAbility.TriggerReason].Add(triggerAbility);
        }
        
    }
    
    private void UntrackEntity(IEntity entity)
    {
        List<TriggerReason> triggerReasons = new(); 
        var abilityComponent = entity.GetEntityComponent<AbilityComponent>();
        foreach (var triggerAbility in abilityComponent.GetAbilitiesByType<IEffectTrigger>())
        {
            if (!_triggerAbilitiesCollection.ContainsKey(triggerAbility.TriggerReason))
            {
                continue;
            }
            triggerReasons.Add(triggerAbility.TriggerReason);
        }
        for (int i = 0; i < triggerReasons.Count; i++)
        {
            foreach (var trigger in _triggerAbilitiesCollection[triggerReasons[i]])
            {
                if (trigger.SourceEntity == entity)
                {
                    _triggerAbilitiesCollection[triggerReasons[i]].Remove(trigger);
                }
            }
        }
    }
}