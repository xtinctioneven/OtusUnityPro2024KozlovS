using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

namespace SampleGame
{
    public class ScreenSpawner : MonoBehaviour
    {
        [SerializeField] private AssetReference _screenAssetReference;
        [SerializeField] private Transform _container;
        [SerializeField] private bool _isScreenActive;
        private AsyncOperationHandle<SceneInstance> _screenHandle;
        private DiContainer _diContainer;
        private UILoaderService _uiLoaderService;

        [Inject]
        private void Construct(DiContainer diContainer, UILoaderService uiLoaderService)
        {
            _diContainer = diContainer;
            _uiLoaderService = uiLoaderService;
            _uiLoaderService.OnAssetsLoaded += TrySpawnScreen;
        }

        private void OnEnable()
        {
            TrySpawnScreen();
        }

        private void TrySpawnScreen()
        {
            if (!_uiLoaderService.IsLoaded)
            {
                return;
            }

            foreach (var uiObject in _uiLoaderService.UIObjects)
            {
                if (uiObject.name == _screenAssetReference.editorAsset.name)
                {
                    GameObject spawnedObject = _diContainer.InstantiatePrefab(uiObject, _container);
                    spawnedObject.name = uiObject.name;
                    spawnedObject.SetActive(_isScreenActive);
                    _uiLoaderService.OnAssetsLoaded -= TrySpawnScreen;
                    Debug.Log("Successfully spawned screen " + spawnedObject.name);
                    return;
                }
            }

            Debug.Log("Failed to spawn screen " + _screenAssetReference.Asset.name);
        }
    }
}
