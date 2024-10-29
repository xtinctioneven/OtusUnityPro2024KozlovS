using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class GameSessionService : MonoBehaviour
{
    [ShowInInspector, ReadOnly] private GameSessionsView _gameSessionsView;
    private DateTime _currentSessionStartTime;
    private List<SessionTimeData> _gameSessions = new();

    [Inject]
    public void Construct(GameSessionsView gameSessionsView)
    {
        _gameSessionsView = gameSessionsView;
    }

    private void Awake()
    {
        _currentSessionStartTime = DateTime.Now;
        AddCurrentSessionTimeData();
        UpdateGameSessionsView();
    }

    public List<SessionTimeData> GetAllSessionsData()
    {
        UpdateCurrentSessionData();
        return _gameSessions;
    }

    public void SetupData(List<SessionTimeData> gameSessionsData)
    {
        _gameSessions = gameSessionsData;
        foreach (var gameSession in gameSessionsData)
        {
            if (gameSession.SessionStart == _currentSessionStartTime)
            {
                UpdateCurrentSessionData();
                UpdateGameSessionsView();
                return;
            }
        }
        AddCurrentSessionTimeData();
        UpdateGameSessionsView();
    }

    private void AddCurrentSessionTimeData()
    {
        var currentSessionTimeData = new SessionTimeData
        {
            SessionNumber = _gameSessions.Count+1,
            SessionStart = _currentSessionStartTime,
            SessionFinish = DateTime.MaxValue,
            SessionDuration = TimeSpan.Zero
        };
        _gameSessions.Add(currentSessionTimeData);
    }

    private void UpdateCurrentSessionData()
    {
        _gameSessions.Last().SessionFinish = DateTime.Now;
        _gameSessions.Last().SessionDuration = _gameSessions.Last().SessionFinish - _gameSessions.Last().SessionStart;
    }

    [Button]
    public void UpdateGameSessionsView()
    {
        UpdateCurrentSessionData();
        List<SessionViewData> sessionViewDataCollection = new List<SessionViewData>();
        foreach (var gameSession in _gameSessions)
        {
            var sessionViewData = new SessionViewData
            {
                SessionNumber = gameSession.SessionNumber.ToString(),
                SessionStart = gameSession.SessionStart.ToString("dd.MM.yyyy HH:mm:ss"),
                SessionFinish = gameSession.SessionFinish == DateTime.MaxValue ? "" 
                    : gameSession.SessionFinish.ToString("dd.MM.yyyy HH:mm:ss"),
                SessionDuration = gameSession.SessionDuration == TimeSpan.Zero ? "" :
                    gameSession.SessionDuration.ToString(@"dd\.hh\:mm\:ss")
            };
            sessionViewDataCollection.Add(sessionViewData);
        }
        _gameSessionsView.Show(sessionViewDataCollection);
    }

    [Button]
    public void ClearSessionData()
    {
        _gameSessions.Clear();
        _gameSessionsView.Clear();
        AddCurrentSessionTimeData();
        UpdateGameSessionsView();
    }
}