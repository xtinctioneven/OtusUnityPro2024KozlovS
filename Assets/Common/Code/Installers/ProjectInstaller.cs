using Zenject;
using SaveSystem;
using UnityEngine;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameRepository>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerPrefsSaver>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Base64DataEncrypter>().FromNew().AsSingle().NonLazy();
    }
}