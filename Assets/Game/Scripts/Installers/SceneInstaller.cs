using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public const string GAME_OVER_POPUP_NAME = "GameOverPopUp";
    [SerializeField] private GameObject _gameOverPopup;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerCharacter>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMoveController>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerShootController>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerStatsPresenter>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EnemySystem>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerDeathObserver>().FromNew().AsSingle().NonLazy();
        Container.Bind<GameObject>().WithId(GAME_OVER_POPUP_NAME).FromInstance(_gameOverPopup).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameOverPopup>().FromNew().AsSingle().NonLazy();
    }
}