using System.Collections.Generic;
using Newtonsoft.Json;

namespace SaveSystem
{
    public class GameRepository: IGameRepository
    {
        private IGameSaver<Dictionary<string, string>> gameStateSaver;
        private Dictionary<string, string> gameState = new();

        GameRepository(IGameSaver<Dictionary<string, string>> gameStateSaver)
        {
            this.gameStateSaver = gameStateSaver;
        }
        
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
            this.gameStateSaver.SaveState(gameState);
        }

        public void LoadState()
        {
            this.gameState = this.gameStateSaver.LoadState();
        }
    }

    public interface IGameRepository
    {
        bool TryGetData<T>(out T data);
        void SetData<T>(T data);
    }
}