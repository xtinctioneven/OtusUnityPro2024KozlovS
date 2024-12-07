using System;
using UnityEngine;

public abstract class EventTask
{
    private Action _onComplete;
    
    public void Run(Action onComplete)
    {
        _onComplete = onComplete;
        OnRun();
    }

    public void Finish()
    {
        if (_onComplete is not null)
        {
            _onComplete?.Invoke();
        }
        OnFinish();
    }

    protected virtual void OnRun()
    {
        
    }

    protected virtual void OnFinish()
    {
        
    }
}