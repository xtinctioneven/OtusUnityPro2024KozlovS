
using SaveSystem;
using Zenject;

public class Task2_SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Chest system
        Container.BindInterfacesAndSelfTo<ChestManager>().FromComponentsInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<ChestGroupView>().FromComponentsInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<ChestModelFactory>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<ChestPresenterFactory>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<ChestTimerFactory>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<ChestTimerService>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ChestTimerSaveLoader>().FromNew().AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<ServerTimeService>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<ZenjectDependencyResolver>().FromNew().AsSingle().NonLazy();
    }
}