using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [ShowInInspector] private ISaveLoader[] saveLoaders;
        private IDependencyResolver dependencyResolver;
        private GameRepository gameRepository;

        [Inject]
        public void Construct(IDependencyResolver dependencyResolver, ISaveLoader[] saveLoaders,
            GameRepository gameRepository)
        {
            this.dependencyResolver = dependencyResolver;
            this.saveLoaders = saveLoaders;
            this.gameRepository = gameRepository;
        }

        [Button]
        public void SaveGame()
        {
            foreach (var saveLoader in this.saveLoaders)
            {
                saveLoader.SaveGame(this.dependencyResolver, this.gameRepository);
            }

            this.gameRepository.SaveState();
        }

        [Button]
        public void LoadGame()
        {
            this.gameRepository.LoadState();
            foreach (var saveLoader in this.saveLoaders)
            {
                saveLoader.LoadGame(this.dependencyResolver, this.gameRepository);
            }
        }
    }
}