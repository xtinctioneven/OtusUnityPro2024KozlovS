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
    public CharacterFactory CharacterFactory;
    public CharacterConfig[] LeftTeamCharacters;
    public Vector2[] LeftTeamPositions;
    public CharacterConfig[] RightTeamCharacters;
    public Vector2[] RightTeamPositions;
    public TeamGridData[] LeftTeamGridData { get; private set; }
    public TeamGridData[] RightTeamGridData { get; private set; }
    [TextArea(4, 10)] public string Log = "";
    
    public bool IsGameOver = false;

    public BattlefieldModel BattlefieldModel;
    
    [Inject]
    public void Construct(
    )
    {
    }

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