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
    public CharacterConfig[] LeftTeamCharacters;
    public Vector2[] LeftTeamPositions;
    public CharacterConfig[] RightTeamCharacters;
    public Vector2[] RightTeamPositions;
    // public CharacterConfig LeftCharacterConfig;
    // public CharacterConfig RightCharacterConfig;
    public TeamGridData[] LeftTeamGridData { get; private set; }
    public TeamGridData[] RightTeamGridData { get; private set; }
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
        int length;
        length = LeftTeamCharacters.Length <= LeftTeamPositions.Length ? LeftTeamCharacters.Length : LeftTeamPositions.Length;
        LeftTeamGridData = new TeamGridData[length];
        for (int i = 0; i < length; i++)
        {
            LeftTeamGridData[i] = new TeamGridData()
            {
                Entity = CharacterFactory.CreateCharacter(LeftTeamCharacters[i]),
                Position = LeftTeamPositions[i]
            };
        }
        length = RightTeamCharacters.Length <= RightTeamPositions.Length ? RightTeamCharacters.Length : RightTeamPositions.Length;
        RightTeamGridData = new TeamGridData[length];
        for (int i = 0; i < length; i++)
        {
            RightTeamGridData[i] = new TeamGridData()
            {
                Entity = CharacterFactory.CreateCharacter(RightTeamCharacters[i]),
                Position = RightTeamPositions[i]
            };
        }
        // LeftTeamGridData[0] = new TeamGridData
        // {
        //     Entity = CharacterFactory.CreateCharacter(LeftCharacterConfig),
        //     Position = new Vector2(1, 1)
        // };
        // RightTeamGridData[0] = new TeamGridData
        // {
        //     Entity = CharacterFactory.CreateCharacter(RightCharacterConfig),
        //     Position = new Vector2(1, 1)
        // };
    }

    public void AddLog(string text)
    {
        Log += text;
        Debug.Log(Log);
    }

}