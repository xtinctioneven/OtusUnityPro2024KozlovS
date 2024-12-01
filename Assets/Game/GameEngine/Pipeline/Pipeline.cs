using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pipeline
{
    public event Action OnFinished;
    private readonly List<EventTask> _tasks = new List<EventTask>();
    private int _taskIndex = -1;
    protected int _tasksToSkip = 0;

    public void AddTask(EventTask eventTask)
    {
        _tasks.Add(eventTask);
    }

    public void ClearAll()
    {
        _tasks.Clear();
        Reset();
    }

    public void Reset()
    {
        _taskIndex = -1;
    }
    
    public void RunNextTask()
    {
        _taskIndex++;
        if (_tasksToSkip > 0)
        {
            _taskIndex += _tasksToSkip;
            _tasksToSkip = 0;
        }
        if (_taskIndex >= _tasks.Count)
        {
            OnFinished?.Invoke();
            return;
        }
        
        Debug.Log($"<color=yellow>Run task # {_taskIndex} of {this}: {_tasks[_taskIndex].ToString()}</color>");
        _tasks[_taskIndex].Run(OnTaskFinished);
    }

    private void OnTaskFinished()
    {
        RunNextTask();
    }

    protected bool TryToSkipToTask(Type taskType)
    {
        for (int i = 0; i < _tasks.Count; i++)
        {
            if (_tasks[i].GetType() == taskType)
            {
                _tasksToSkip = i - _taskIndex - 1;
                return true;
            }
        }
        return false;
    }
}