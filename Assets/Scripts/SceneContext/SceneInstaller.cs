using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class SceneInstaller : MonoInstaller
    {
        //[SerializeField] private PlayerConfigCollection _playerConfigCollection;
        public override void InstallBindings()
        {
            //Container.Bind<PlayerConfigCollection>().FromInstance(_playerConfigCollection).AsSingle().NonLazy();
            Container.Bind<List<UserInfo>>().FromNew().AsSingle().NonLazy();
            Container.Bind<List<PlayerLevel>>().FromNew().AsSingle().NonLazy();
            Container.Bind<List<CharacterInfo>>().FromNew().AsSingle().NonLazy();
            Container.Bind<CharacterStatPresenterFactory>().FromNew().AsSingle().NonLazy();
            Container.Bind<PlayerPopupPresenterFactory>().FromNew().AsSingle().NonLazy();
            Container.Bind<ConfigParser>().FromNew().AsSingle().NonLazy();
            
            //container.Bind<ProductBuyer>().AsSingle().NonLazy();
            //container.Bind<ProductPresenterFactory>().AsSingle().NonLazy();

            //public CurrencyInstaller(DiContainer container)
            //{
            //    var view = Object.FindObjectOfType<CurrencyProvider>();
            //    MoneyBind(container, view.MoneyView);
            //    GemBind(container, view.GemView);
            //}

            //private static void MoneyBind(DiContainer diContainer, CurrencyView viewMoneyView)
            //{
            //    diContainer
            //        .Bind<MoneyStorage>()
            //        .AsSingle()
            //        .WithArguments(10L)
            //        .NonLazy();

            //    diContainer.BindInterfacesTo<MoneyPanelAdapter>().AsSingle().WithArguments(viewMoneyView).NonLazy();
            //}

            //private static void GemBind(DiContainer diContainer, CurrencyView viewGemView)
            //{
            //    diContainer
            //        .Bind<GemStorage>()
            //        .AsSingle()
            //        .WithArguments(10L)
            //        .NonLazy();

            //    diContainer.BindInterfacesTo<GemPanelAdapter>().AsSingle().WithArguments(viewGemView).NonLazy();
            //}
        }
    }
}