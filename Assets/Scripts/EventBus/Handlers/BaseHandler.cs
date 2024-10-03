using System;
using Zenject;

public abstract class BaseHandler<TEvent> : IInitializable, IDisposable
{
    protected readonly EventBus EventBus;

    [Inject]
    protected BaseHandler(EventBus eventBus)
    {
        EventBus = eventBus;
    }
    
    public void Initialize()
    {
        EventBus.Subscribe<TEvent>(OnHandleEvent);
    }

    public void Dispose()
    {
        EventBus.Unsubscribe<TEvent>(OnHandleEvent);
    }
    
    protected abstract void OnHandleEvent(TEvent evt);
}