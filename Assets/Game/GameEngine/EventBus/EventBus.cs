using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class EventBus 
{
    private readonly Dictionary<Type, IEventHandlerCollection> _handlers = new ();
    private readonly Queue<IEvent> _eventsQueue = new();
    private readonly Queue<IEvent> _priorityEventsQueue = new();
    private bool _isRunning;
    
    public void Subscribe<TEvent>(Action<TEvent> handler)
    {
        var key = typeof(TEvent);
        if (!_handlers.ContainsKey(key))
        {
            _handlers[key] = new EventHandlerCollection<TEvent>();
        }
        _handlers[key].Subscribe(handler);
    }

    public void Unsubscribe<TEvent>(Action<TEvent> handler)
    {
        var key = typeof(TEvent);
        if (_handlers.TryGetValue(key, out var handlers))
        {
            handlers.Unsubscribe(handler);
        }
    }

    public void RaiseEvent<TEvent>(TEvent evt) where TEvent : IEvent
    {
        if (_isRunning)
        {
            _eventsQueue.Enqueue(evt);
            return;
        }
        _isRunning = true;
        var key = evt.GetType();
        Debug.Log($"<color=green>Event raised: {key}</color>");
        
        if (_handlers.TryGetValue(key, out var handlers))
        {
            handlers.RaiseEvent(evt);
        }
        _isRunning = false;

        TryRaiseNextEvent();
    }
    
    public void RaiseEventWithPriority<TEvent>(TEvent evt) where TEvent : IEvent
    {
        if (_isRunning)
        {
            _priorityEventsQueue.Enqueue(evt);
            return;
        }
        _isRunning = true;
        var key = evt.GetType();
        Debug.Log($"Priority event raised: {key}");
        
        if (_handlers.TryGetValue(key, out var handlers))
        {
            handlers.RaiseEvent(evt);
        }
        _isRunning = false;
        
        TryRaiseNextEvent();
    }

    private void TryRaiseNextEvent()
    {
        if (_priorityEventsQueue.TryDequeue(out var result))
        {
            RaiseEventWithPriority(result);
        }
        else
        if (_eventsQueue.TryDequeue(out result))
        {
            RaiseEvent(result);
        }
    }
}
