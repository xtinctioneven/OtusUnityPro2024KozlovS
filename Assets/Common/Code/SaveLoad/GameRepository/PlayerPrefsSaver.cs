using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public class PlayerPrefsSaver : IGameSaver<Dictionary<string, string>>
    {
        private const string GAME_STATE_KEY = "GameStateKey";

        private IDataEncrypter dataEncrypter;

        PlayerPrefsSaver(IDataEncrypter dataEncrypter)
        {
            this.dataEncrypter = dataEncrypter;
        }

        public void SaveState(Dictionary<string, string> gameState)
        {
            string gameStateJson = JsonConvert.SerializeObject(gameState);
            gameStateJson = this.dataEncrypter.Encrypt(gameStateJson);
            PlayerPrefs.SetString(GAME_STATE_KEY, gameStateJson);
            Debug.Log("Saved save data!");
        }

        public Dictionary<string, string> LoadState()
        {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                string gameStateJson = PlayerPrefs.GetString(GAME_STATE_KEY);
                gameStateJson = this.dataEncrypter.Decrypt(gameStateJson);
                Debug.Log("Loaded save data!");
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(gameStateJson);
            }
            else
            {
                Debug.Log("No save data found!");
                return new Dictionary<string, string>();
            }
        }
    }


    public interface IGameSaver<TDataFormat>
    {
        public void SaveState(TDataFormat gameState);
        public TDataFormat LoadState();
    }
}