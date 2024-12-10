using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDispatcher : MonoBehaviour
{
    private Dictionary<string, List<Action>> _actionsDictionary = new();

    public void SubscribeOnEvent(string key, Action action)
    {
        if (!_actionsDictionary.ContainsKey(key))
        {
            _actionsDictionary.Add(key, new List<Action>());
        }
        _actionsDictionary[key].Add(action);
    }

    public void UnsubscribeOnEvent(string key, Action action)
    {
        if (_actionsDictionary.TryGetValue(key, out List<Action> actions))
        {
            actions.Remove(action);
        }
    }

    public void DispatchEvent(string actionKey)
    {
        if (_actionsDictionary.TryGetValue(actionKey, out List<Action> actions))
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i]?.Invoke();
            }
        }
    }
}
