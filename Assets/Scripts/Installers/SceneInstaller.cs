using GameEngine;
using SaveSystem;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Unit>().FromComponentsInHierarchy().AsCached();
        Container.BindInterfacesAndSelfTo<UnitManager>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<UnitSaveLoader>().FromNew().AsSingle();
        Container.Bind<Resource>().FromComponentsInHierarchy().AsCached();
        Container.BindInterfacesAndSelfTo<ResourceService>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<ResourceSaveLoader>().FromNew().AsSingle();
    }
}