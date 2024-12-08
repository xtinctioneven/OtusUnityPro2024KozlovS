using System.Collections.Generic;
using System.Linq;
using Game.Gameplay;
using Sirenix.Utilities;

public class TurnOrderService
{
    // EventBus _eventBus;
    public bool IsQueueEmpty => _entitiesQueue.Count == 0;
    private readonly EntityTrackerService _entityTrackerService;
    private Queue<IEntity> _entitiesQueue = new();
    private IEntity _activeEntity;

    public TurnOrderService(
        EntityTrackerService entityTrackerService
    )
    {
        _entityTrackerService = entityTrackerService;
        _entityTrackerService.OnEntityUntracked += EntityTrackerOnEntityUntracked;
        _entityTrackerService.OnEntityTracked += EntityTrackerServiceOnOnEntityTracked;
    }

    private void EntityTrackerServiceOnOnEntityTracked(IEntity trackedEntity)
    {
        List<IEntity> entities = _entitiesQueue.ToList();
        _entitiesQueue.Clear();
        entities.Add(trackedEntity);
        entities = entities.OrderByDescending(x => x.GetEntityComponent<SpeedComponent>().Value).ToList();
        entities.ForEach(entity => _entitiesQueue.Enqueue(entity));
    }

    private void EntityTrackerOnEntityUntracked(IEntity untrackedEntity)
    {
        if (_entitiesQueue.Contains(untrackedEntity))
        {
            List<IEntity> entities = _entitiesQueue.ToList();
            _entitiesQueue.Clear();
            entities.Remove(untrackedEntity);
            entities = entities.OrderByDescending(x => x.GetEntityComponent<SpeedComponent>().Value).ToList();
            entities.ForEach(entity => _entitiesQueue.Enqueue(entity));
        }
    }

    public void EnqueueEntities(IReadOnlyList<IEntity> newEntities)
    {
        if (newEntities.Count == 0)
        {
            return;
        }

        List<IEntity> allEntities = new List<IEntity>();
        allEntities.AddRange(newEntities);
        allEntities.AddRange(_entitiesQueue);
        _entitiesQueue =
            new Queue<IEntity>(allEntities.OrderByDescending(x => x.GetEntityComponent<SpeedComponent>().Value));
    }

    public IEntity ActivateNextEntity()
    {
        PrepareQueue();
        if (_entitiesQueue.Count == 0)
        {
            EnqueueEntities(_entityTrackerService.GetAllEntities());
        }

        _activeEntity = _entitiesQueue.Dequeue();
        return _activeEntity;
    }

    public void Clear()
    {
        _entitiesQueue.Clear();
        _activeEntity = null;
    }

    public IEntity GetActiveEntity()
    {
        return _activeEntity;
    }

    public void PrepareQueue()
    {
        while (_entitiesQueue.Count > 0 && _entitiesQueue.Peek() == null)
        {
            _entitiesQueue.Dequeue();
        }
    }
}