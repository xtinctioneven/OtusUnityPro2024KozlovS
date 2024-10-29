using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [ShowInInspector] private ISaveLoader[] _saveLoaders;
        private IDependencyResolver _dependencyResolver;
        private GameRepository _gameRepository;

        [Inject]
        public void Construct(IDependencyResolver dependencyResolver, ISaveLoader[] saveLoaders,
            GameRepository gameRepository)
        {
            _dependencyResolver = dependencyResolver;
            _saveLoaders = saveLoaders;
            _gameRepository = gameRepository;
        }

        private void Start()
        {
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        [Button]
        public void SaveGame()
        {
            foreach (var saveLoader in this._saveLoaders)
            {
                saveLoader.SaveGame(this._dependencyResolver, this._gameRepository);
            }

            this._gameRepository.SaveState();
        }

        [Button]
        public void LoadGame()
        {
            this._gameRepository.LoadState();
            foreach (var saveLoader in this._saveLoaders)
            {
                saveLoader.LoadGame(this._dependencyResolver, this._gameRepository);
            }
        }

        [Button]
        public void ClearSaveData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}