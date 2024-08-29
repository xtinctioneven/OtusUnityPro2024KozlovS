using Zenject;
using SaveSystem;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameRepository>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerPrefsSaver>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Base64DataEncrypter>().FromNew().AsSingle().NonLazy();
    }
}