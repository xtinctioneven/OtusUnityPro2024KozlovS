using System;
using DG.Tweening;
using Game.Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Helper : MonoBehaviour
{
    public event Action OnLoadingFinished;
    public static Helper Instance;
    public TeamGridData[] LeftTeamGridData { get; private set; }
    public TeamGridData[] RightTeamGridData { get; private set; }
    public bool IsSkipVisual = false;
    public bool IsGameOver = false;

    
    [Space]
    [Header("Forward move settings")]
    public float ForwardJumpPower = 2f;
    public float ForwardJumpDuration = .3f;
    public Ease ForwardJumpEase = Ease.OutSine;
    [Space]
    [Header("High float settings")]
    public float BackJumpPower = 2f;
    public float BackJumpDuration = .3f;
    public Ease BackJumpEase = Ease.OutSine;
    [Space]
    [Header("High float settings")]
    public float NoLinkWigglePower = 0.4f;
    public float NoLinkWiggleDuration = 0.4f;
    [Space]
    [Header("High float settings")]
    public float LinkHighFloatAltitude = 1.5f;
    public float LinkHighFloatDuration = 0.6f;
    public Ease HighFloatEase = Ease.OutExpo;
    [Space]
    [Header("Low float settings")]
    public float LinkLowFloatAltitude = 1f;
     public float LinkLowFloatDuration = 0.5f;
    public Ease LowFloatEase = Ease.OutExpo;
    [Space]
    [Header("Repulse settings")]
    public float LinkRepulseDuration = .7f;
    public float RepulseLeftTeamZ = -5.3f;
    public float RepulseRightTeamZ = 8.5f;
    public Ease RepulseEase = Ease.OutExpo;
    
    [TextArea(4, 10)] public string Log = "";
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnLoadingFinished?.Invoke();
    }

    public void AddLog(string text)
    {
        Log += text;
        Debug.Log(text);
    }

}