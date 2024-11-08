using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace SampleGame
{
    public class UILoaderService : IInitializable
    {
        public event Action OnAssetsLoaded;
        public GameObject[] UIObjects { get; private set; }
        public bool IsLoaded { get; private set; }
        private readonly AddressableAssetGroup _assetGroup;

        public UILoaderService(AddressableAssetGroup group)
        {
            _assetGroup = group;
            IsLoaded = false;
        }

        public void Initialize()
        {
            LoadAssets();
        }

        private async void LoadAssets()
        {
            var loadedAssets = new List<AsyncOperationHandle<GameObject>>();
            foreach (AddressableAssetEntry asset in _assetGroup.entries)
            {
                loadedAssets.Add(Addressables.LoadAssetAsync<GameObject>(asset.address));
            }

            UIObjects = await Task.WhenAll(loadedAssets.Select(x => x.Task));
            IsLoaded = true;
            OnAssetsLoaded?.Invoke();
        }
    }
}