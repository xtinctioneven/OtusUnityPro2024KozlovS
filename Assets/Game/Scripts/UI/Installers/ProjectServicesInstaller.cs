using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public class ProjectServicesInstaller : MonoInstaller
    {
        [SerializeField] private AddressableAssetGroup _uiAddressablesAssetGroup;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UILoaderService>().FromNew().AsSingle().WithArguments(_uiAddressablesAssetGroup).NonLazy();
            Container.BindInterfacesAndSelfTo<AssetsLoaderService>().FromNew().AsSingle().NonLazy();
        }
    }
}