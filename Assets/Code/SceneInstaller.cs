using Game.GamePlay.Conveyor;
using Game.GamePlay.Upgrades;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MoneyStorage>().FromComponentsInHierarchy().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ConveyorEntity>().FromComponentsInHierarchy().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UpgradeSystem>().FromNew().AsSingle().NonLazy();
    }
}