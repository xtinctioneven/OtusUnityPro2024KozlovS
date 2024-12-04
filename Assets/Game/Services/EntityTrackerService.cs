using System;
using System.Collections.Generic;
using Game.Gameplay;
using Zenject;

public class EntityTrackerService
{
    public event Action<IEntity> OnEntityTracked; 
    public event Action<IEntity> OnEntityUntracked; 
    private EventBus _eventBus;
    private DiContainer _diContainer;
    private BattlefieldModel _battlefieldModel;
    private List<IEntity> _leftTeam = new();
    private List<IEntity> _rightTeam = new();
    private IEntity _activeEntity;
    
    public EntityTrackerService(
        EventBus eventBus,
        DiContainer diContainer
    )
    {
        _eventBus = eventBus;
        _diContainer = diContainer;
    }

    public void Initialize()
    {
        _battlefieldModel = _diContainer.Resolve<BattlefieldModel>();
        foreach (var entity in _battlefieldModel.GetLeftTeamEntities())
        {
            TrackEntity(entity);
        }
        foreach (var entity in _battlefieldModel.GetRightTeamEntities())
        {
            TrackEntity(entity);
        }
    }

    public void Clear()
    {
        _leftTeam = null;
        _rightTeam = null;
        _activeEntity = null;
    }

    public IEntity GetActiveEntity()
    {
        return _activeEntity;
    }

    public void TrackEntity(IEntity entity)
    {
        if (entity.GetEntityComponent<Game.Gameplay.TeamComponent>().Value == Team.Left)
        {
            _leftTeam.Add(entity);
        }
        else
        {
            _rightTeam.Add(entity);
        }
        OnEntityTracked?.Invoke(entity);
    }

    public void UntrackEntity(IEntity entity)
    {
        if (_leftTeam.Contains(entity))
        {
            _leftTeam.Remove(entity);
        }
        else
        {
            _rightTeam.Remove(entity);
        }
        OnEntityUntracked?.Invoke(entity);
    }

    public IReadOnlyList<IEntity> GetLeftTeam()
    {
        return _leftTeam;
    }

    public IReadOnlyList<IEntity> GetRightTeam()
    {
        return _rightTeam;
    }

    public IReadOnlyList<IEntity> GetAllEntities()
    {
        List<IEntity> entities = new();
        entities.AddRange(_leftTeam);
        entities.AddRange(_rightTeam);
        return entities;
    }
}