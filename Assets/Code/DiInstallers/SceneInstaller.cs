using Client;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<EcsStartup>().FromNew().AsSingle().NonLazy();
    }
}