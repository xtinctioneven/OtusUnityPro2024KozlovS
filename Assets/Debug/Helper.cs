using System;
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

    public float ForwardJumpPower = 2f;
    public float ForwardJumpDuration = .3f;
    public float BackJumpPower = 2f;
    public float BackJumpDuration = .3f;
    public float LinkNoneVar1 = 0.3f;
    public float LinkNoneVar2 = 0.3f;
    public float LinkRepulseVar1 = 1.5f;
    public float LinkRepulseVar2 = 1.5f;
    public float LinkHighFloatVar1 = 1.5f;
    public float LinkHighFloatVar2 = 1.5f;
    public float LinkLowFloatVar1 = 1.5f;
    public float LinkLowFloatVar2 = 1.5f;
    public float RepulseLeftZ = 1.5f;
    public float RepulseRightZ = 1.5f;
    
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