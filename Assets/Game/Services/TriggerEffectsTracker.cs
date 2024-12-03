using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
        var allEntities = _entityTrackerService.GetAllEntities();
        for (int i = 0; i < allEntities.Count; i++)
        {
            var abilityComponent = allEntities[i].GetEntityComponent<AbilityComponent>();
            foreach (var triggerAbility in abilityComponent.GetAbilitiesByType<IEffectTrigger>())
            {
                if (!_triggerAbilitiesCollection.ContainsKey(triggerAbility.TriggerReason))
                {
                    _triggerAbilitiesCollection.Add(triggerAbility.TriggerReason, new List<IEffectTrigger>());
                }
                _triggerAbilitiesCollection[triggerAbility.TriggerReason].Add(triggerAbility);
            }
        }
    }

    // public void TrackEntity(IEntity entity)
    // {
    //     if (entity.GetEntityComponent<Game.Gameplay.TeamComponent>().Value == Team.Left)
    //     {
    //         _leftTeam.Add(entity);
    //     }
    //     else
    //     {
    //         _rightTeam.Add(entity);
    //     }
    // }
    //
    // public void UntrackEntity(IEntity entity)
    // {
    //     if (_leftTeam.Contains(entity))
    //     {
    //         _leftTeam.Remove(entity);
    //     }
    //     else
    //     {
    //         _rightTeam.Remove(entity);
    //     }
    // }

    public List<IEffectTrigger> GetTriggers(TriggerReason reason, IEntity activeEntity)
    {
        List<IEffectTrigger> triggers = new List<IEffectTrigger>();
        if (!_triggerAbilitiesCollection.ContainsKey(reason))
        {
            return triggers;
        }
        var possibleTriggers = _triggerAbilitiesCollection[reason];
        for (int i = 0; i < possibleTriggers.Count; i++)
        {
            if (possibleTriggers[i].SourceEntity != activeEntity || !possibleTriggers[i].CanBeUsed)
            {
                continue;
            }
            triggers.Add(possibleTriggers[i]);
        }
        return triggers;
    }
}