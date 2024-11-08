using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SampleGame;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = System.Object;

namespace SampleGame
{
    public class AssetsLoaderService
    {
        private const string GAME_SCENE_ASSETS_LABEL = "GameScene";
        private const string GAME_LOCATIONS_ASSETS_LABEL = "LocationsCommon";

        public event Action OnGameSceneUnload;
        private AsyncOperationHandle<IList<object>> _gameSceneAssetsHandle;
        private AsyncOperationHandle<IList<object>> _locationsAssetsHandle;
        private readonly List<AsyncOperationHandle> _loadedAssetsHandles = new();
        private bool _isLocationAssetsLoaded = false;

        public async Task<TObject> TryLoadAssetByName<TObject>(string assetName)
        {
            var handle = Addressables.LoadAssetAsync<TObject>(assetName);
            await handle.Task;
            _loadedAssetsHandles.Add(handle);
            return handle.Result;
        }

        public async Task TryLoadLocationsCommonAssets()
        {
            if (_isLocationAssetsLoaded)
            {
                return;
            }

            _locationsAssetsHandle = Addressables.LoadAssetsAsync<Object>(GAME_LOCATIONS_ASSETS_LABEL, obj =>
                Debug.Log("Loaded asset: " + obj));
            await _locationsAssetsHandle.Task;
            _isLocationAssetsLoaded = true;
        }

        public async Task LoadGameSceneAssets()
        {
            _gameSceneAssetsHandle = Addressables.LoadAssetsAsync<Object>(GAME_SCENE_ASSETS_LABEL, obj =>
                Debug.Log("Loaded asset: " + obj));
            await _gameSceneAssetsHandle.Task;
        }

        public void UnloadGameSceneAssets()
        {
            for (int i = 0; i < _loadedAssetsHandles.Count; i++)
            {
                if (_loadedAssetsHandles[i].IsValid())
                {
                    Addressables.Release(_loadedAssetsHandles[i]);
                    Debug.Log("Unloaded asset handle #" + i + ": " + _loadedAssetsHandles[i].ToString());
                }
            }

            UnloadLocationsCommonAssets();
            Debug.Log("Unloaded Locations Common Assets");
            if (_gameSceneAssetsHandle.IsValid())
            {
                Addressables.Release(_gameSceneAssetsHandle);
                Debug.Log("Released GameScene Assets Handle");
            }

            OnGameSceneUnload?.Invoke();
            Debug.Log("Invoked OnGameSceneUnload");
        }

        private void UnloadLocationsCommonAssets()
        {
            if (_locationsAssetsHandle.IsValid())
            {
                Addressables.Release(_locationsAssetsHandle);
            }
        }
    }
}