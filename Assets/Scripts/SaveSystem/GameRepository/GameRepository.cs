using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class GameRepository: IGameRepository
{
    private const string GAME_STATE_KEY  = "GameStateKey";
    
    private Dictionary<string, string> gameState = new();
    
    public bool TryGetData<T>(out T data)
    {
        string key = typeof(T).ToString();
        if (this.gameState.TryGetValue(key, out string jsonData))
        {
            data = JsonConvert.DeserializeObject<T>(jsonData);
            return true;
        }

        data = default;
        return false;
    }

    public void SetData<T>(T data)
    {
        string jsonData = JsonConvert.SerializeObject(data, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        string key = typeof(T).ToString();
        this.gameState[key] = jsonData;
    }

    public void SaveState()
    {
        string gameStateJson = JsonConvert.SerializeObject(this.gameState);
        gameStateJson = Convert.ToBase64String(Encoding.UTF8.GetBytes(gameStateJson));
        PlayerPrefs.SetString(GAME_STATE_KEY, gameStateJson);
        Debug.Log("Saved save data!");
    }

    public void LoadState()
    {
        if (PlayerPrefs.HasKey(GAME_STATE_KEY))
        {
            string gameStateJson = PlayerPrefs.GetString(GAME_STATE_KEY);
            gameStateJson = Encoding.UTF8.GetString(Convert.FromBase64String(gameStateJson));
            this.gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(gameStateJson);
            Debug.Log("Loaded save data!");
        }
        else
        {
            Debug.Log("No save data found!");
        }
    }
}

public interface IGameRepository
{
    bool TryGetData<T>(out T data);
    void SetData<T>(T data);
}