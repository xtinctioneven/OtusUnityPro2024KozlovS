using UnityEngine;
using Zenject;

public class MainScreenInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerSelectionService>().FromNew().AsSingle().NonLazy();
    }
}