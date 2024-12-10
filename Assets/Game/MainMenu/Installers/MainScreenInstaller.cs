using UnityEngine;
using Zenject;

public class MainScreenInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CharacterSelectionService>().FromNew().AsSingle().NonLazy();
    }
}