using SaveSystem;
using UnityEngine;
using Zenject;

public class Task1_SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ZenjectDependencyResolver>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameSessionService>().FromComponentsInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<GameSessionsView>().FromComponentsInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<GameSessionSaveLoader>().FromNew().AsSingle();
    }
}