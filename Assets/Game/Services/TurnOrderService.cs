using System.Collections.Generic;
using System.Linq;
using Game.Gameplay;

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
    }

    public void Initialize()
    {
        EnqueueEntities(_entityTrackerService.GetAllEntities());
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
        _entitiesQueue = new Queue<IEntity>(allEntities.OrderByDescending(x => x.GetEntityComponent<SpeedComponent>().Value));
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
        while (_entitiesQueue.Count>0 && _entitiesQueue.Peek() == null)
        {
            _entitiesQueue.Dequeue();
        }
    }
}