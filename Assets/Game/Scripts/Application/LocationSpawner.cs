using UnityEngine;
using Zenject;


namespace SampleGame
{
    [RequireComponent(typeof(BoxCollider))]
    public class LocationSpawner : MonoBehaviour
    {
        [SerializeField] private string _locationKey;
        [SerializeField] private Transform _locationContainer;
        private bool _isLoaded = false;
        private AssetsLoaderService _assetsLoaderService;

        [Inject]
        public void Construct(AssetsLoaderService assetsLoaderService)
        {
            _assetsLoaderService = assetsLoaderService;
        }

        private void Awake()
        {
            _isLoaded = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            SpawnLocation();
        }

        private async void SpawnLocation()
        {
            if (_isLoaded)
            {
                return;
            }

            await _assetsLoaderService.TryLoadLocationsCommonAssets();
            GameObject location = await _assetsLoaderService.TryLoadAssetByName<GameObject>(_locationKey);
            if (location == null)
            {
                Debug.LogError("Couldn't load location: " + _locationKey);
                return;
            }

            var spawnedLocation = Instantiate(location, _locationContainer);
            spawnedLocation.name = "[" + location.name + "]";
            Debug.Log("Loaded location: " + spawnedLocation.name);
            _assetsLoaderService.OnGameSceneUnload += OnUnload;
            _isLoaded = true;
        }

        private void OnUnload()
        {
            _isLoaded = false;
        }
    }
}