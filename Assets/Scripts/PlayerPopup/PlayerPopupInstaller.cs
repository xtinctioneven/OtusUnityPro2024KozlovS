using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PlayerPopupInstaller : MonoInstaller
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private UserInfoView _userInfoView;
        [SerializeField] private CharacterInfoView _characterInfoView;
        [SerializeField] private PlayerLevelView _playerLevelView;
        public override void InstallBindings()
        {
            Container.Bind<Button>().FromInstance(_closeButton).AsSingle().NonLazy();
            Container.Bind<UserInfoView>().FromInstance(_userInfoView).AsSingle().NonLazy();
            Container.Bind<CharacterInfoView>().FromInstance(_characterInfoView).AsSingle().NonLazy();
            Container.Bind<PlayerLevelView>().FromInstance(_playerLevelView).AsSingle().NonLazy();
        }
    }
}