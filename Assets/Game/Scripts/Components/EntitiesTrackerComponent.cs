using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

[Serializable]
public class EntitiesTrackerComponent
{
    public AtomicEvent<AtomicEntity> OnEntityTrack = new AtomicEvent<AtomicEntity>();
    public AtomicEvent<AtomicEntity> OnEntityUntrack = new AtomicEvent<AtomicEntity>();
    public List<AtomicEntity> TrackedEntities = new List<AtomicEntity>();
    [SerializeField] private AtomicVariable<Type> _trackedEntityType;
    
    public void Compose()
    {
        _trackedEntityType.Subscribe(SetupTrackedEntities);
    }

    public void TrackType(Type entityType)
    {
        if (entityType.BaseType == typeof(AtomicEntity))
        {
            _trackedEntityType.Value = entityType;
        }
        else
        {
            Debug.LogError("Passed type is not AtomicEntity!");
        }
    }

    private void SetupTrackedEntities(Type entityType)
    { 
        TrackedEntities.Clear();
        var objects = GameObject.FindObjectsOfType(entityType);
        for (int i = 0; i < objects.Length; i++)
        {
            Track(objects[i] as AtomicEntity);
        }
    }

    public void Track(AtomicEntity entityToTrack)
    {
        if (_trackedEntityType.Value == entityToTrack.GetType())
        {
            TrackedEntities.Add(entityToTrack);
            OnEntityTrack?.Invoke(entityToTrack);
        }
    }

    public void Untrack(AtomicEntity entityToUntrack)
    {
        if (TrackedEntities.Contains(entityToUntrack))
        {
            TrackedEntities.Remove(entityToUntrack);
            OnEntityUntrack?.Invoke(entityToUntrack);
        }
    }
}