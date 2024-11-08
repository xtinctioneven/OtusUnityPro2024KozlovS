using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

namespace SampleGame
{
    public sealed class GameLoader
    {
        private AsyncOperationHandle<SceneInstance> _gameSceneHandle;
        private AssetsLoaderService _assetsLoaderService;
        
        [Inject]
        public void Construct(AssetsLoaderService assetsLoaderService)
        {
            _assetsLoaderService = assetsLoaderService;
        }
        
        public async Task UnloadGame()
        {
            _assetsLoaderService.UnloadGameSceneAssets();
            await Addressables.UnloadSceneAsync(_gameSceneHandle, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects).Task;
        }
        
        public async void LoadGame()
        {
            await _assetsLoaderService.LoadGameSceneAssets();
            _gameSceneHandle = Addressables.LoadSceneAsync("Game");
            await _gameSceneHandle.Task;
        }
    }
}