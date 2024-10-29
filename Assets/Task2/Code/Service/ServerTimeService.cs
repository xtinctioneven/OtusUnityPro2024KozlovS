using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class ServerTimeService: ITickable
{
    //Время в Africa/Abidjan совпадает с UTC.
    private const string SERVER_TIME_URL = "https://timeapi.io/api/time/current/zone?timeZone=Africa%2FAbidjan";
    public event Action<TimeSpan, bool> OnTick;
    public event Action<bool> OnSynchronize;
    private bool _isServerTimeReceived;
    private bool _isProblemDetected;
    private DateTime _serverTime;
    private DateTime _localTime;
    private float _currentTickTime;
    private float _timeToServerSynchronize;
    private readonly float _tickTime;
    private readonly float _serverSynchronizeFrequency;

    public ServerTimeService(float tickTime = 1f, float serverSynchronizeFrequency = 5f)
    {
        _tickTime = tickTime;
        _currentTickTime = _tickTime;
        _serverSynchronizeFrequency = serverSynchronizeFrequency;
        _timeToServerSynchronize = _serverSynchronizeFrequency;
        _isServerTimeReceived = false;
        _localTime = DateTime.UtcNow;
    }

    void ITickable.Tick()
    {
        _currentTickTime -= Time.deltaTime;
        _timeToServerSynchronize -= Time.deltaTime;
        if (_currentTickTime <= 0)
        {
            _currentTickTime = _tickTime;
            _localTime += TimeSpan.FromSeconds(_tickTime);
            TimeSpan tickDuration = TimeSpan.FromSeconds(_tickTime);
            OnTick?.Invoke(tickDuration, _isServerTimeReceived);
        }

        if (_timeToServerSynchronize <= 0)
        {
            _timeToServerSynchronize = _serverSynchronizeFrequency;
            _isServerTimeReceived = false;
            SynchronizeTime();
        }
    }

    private async void SynchronizeTime()
    {
        var webRequest = UnityWebRequest.Get(SERVER_TIME_URL);
        await webRequest.SendWebRequest().ToUniTask();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching time: " + webRequest.error);
        }
        else
        {
            ServerTimeResponse timeInfo = JsonUtility.FromJson<ServerTimeResponse>(webRequest.downloadHandler.text);
            _serverTime = DateTime.Parse(timeInfo.dateTime);
            _isServerTimeReceived = true;
            TimeSpan timeDifference = _serverTime - _localTime;
            _localTime = _serverTime;
            if (timeDifference.TotalSeconds <= TimeSpan.FromSeconds(_timeToServerSynchronize).TotalSeconds)
            {
                Debug.Log("Time synchronization complete. Local time was correct, no cheating or errors.");
                _isProblemDetected = false;
            }
            else
            {
                Debug.LogWarning("Time synchronization complete. Local time differs too much from server time! Difference is " 
                                 + timeDifference.TotalSeconds + " seconds!");
                _isProblemDetected = true;
            }
            OnSynchronize?.Invoke(_isProblemDetected);
        }
    }

    public DateTime GetCurrentTime()
    {
        return _localTime;
    }
}

[Serializable]
public class ServerTimeResponse
{
    public string dateTime;
}