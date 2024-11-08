using UnityEngine;
using Zenject;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "ProjectInstaller",
        menuName = "Installers/New ProjectInstaller"
    )]
    public sealed class ProjectInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            this.Container.Bind<ApplicationExiter>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<GameLoader>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<MenuLoader>().AsSingle().NonLazy();
        }
    }
}