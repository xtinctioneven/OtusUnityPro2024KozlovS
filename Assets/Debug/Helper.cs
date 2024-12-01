using System;
using Game.Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Helper : MonoBehaviour
{
    public static Helper Instance;
    public CharacterFactory CharacterFactory;
    public CharacterConfig LeftCharacterConfig;
    public CharacterConfig RightCharacterConfig;
    public TeamGridData[] LeftTeamGridData = new TeamGridData[1];
    public TeamGridData[] RightTeamGridData = new TeamGridData[1];
    [TextArea(4, 10)] public string Log = "";
    
    public bool IsGameOver = false;

    public BattlefieldModel BattlefieldModel;
    
    [Inject]
    public void Construct(CharacterFactory characterFactory)
    {
        CharacterFactory = characterFactory;
    }
    
    private void Awake()
    {
        Instance = this;
        LeftTeamGridData[0] = new TeamGridData
        {
            Entity = CharacterFactory.CreateCharacter(LeftCharacterConfig),
            Position = new Vector2(1, 1)
        };
        RightTeamGridData[0] = new TeamGridData
        {
            Entity = CharacterFactory.CreateCharacter(RightCharacterConfig),
            Position = new Vector2(1, 1)
        };
    }

    public void AddLog(string text)
    {
        Log += text;
        Debug.Log(Log);
    }

}