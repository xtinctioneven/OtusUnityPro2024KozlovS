using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class GameSessionsView : MonoBehaviour
{
    [SerializeField] private SessionView _sessionViewPrefab;
    [SerializeField] private Transform _sessionViewsContainer;
    private List<SessionView> _sessionViews = new List<SessionView>();
    
    public void Show(List<SessionViewData> sessionDataList)
    {
        Clear();
        foreach (var sessionData in sessionDataList)
        {
            var sessionView = Instantiate(_sessionViewPrefab, _sessionViewsContainer);
            sessionView.name = _sessionViewPrefab.name + " " + (_sessionViews.Count + 1);
            sessionView.Show(sessionData);
            _sessionViews.Add(sessionView);
        }
    }

    [Button]
    public void Clear()
    {
        foreach (var sessionView in _sessionViews)
        {
            Destroy(sessionView.gameObject);
        }
        _sessionViews.Clear();
    }
}
