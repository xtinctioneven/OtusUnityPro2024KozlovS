using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [ShowInInspector] private ISaveLoader[] saveLoaders;
        private DiContainer diContainer;
        private GameRepository gameRepository;

        [Inject]
        public void Construct(DiContainer diContainer, ISaveLoader[] saveLoaders,
            GameRepository gameRepository)
        {
            this.diContainer = diContainer;
            this.saveLoaders = saveLoaders;
            this.gameRepository = gameRepository;
        }

        [Button]
        public void SaveGame()
        {
            foreach (var saveLoader in this.saveLoaders)
            {
                saveLoader.SaveGame(this.diContainer, this.gameRepository);
            }

            this.gameRepository.SaveState();
        }

        [Button]
        public void LoadGame()
        {
            this.gameRepository.LoadState();
            foreach (var saveLoader in this.saveLoaders)
            {
                saveLoader.LoadGame(this.diContainer, this.gameRepository);
            }
        }
    }
}