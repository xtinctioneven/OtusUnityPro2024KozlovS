using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PlayerPopupView : MonoBehaviour
    {
        private UserInfoView _userInfoView;
        private CharacterInfoView _characterInfoView;
        private PlayerLevelView _playerLevelView;
        private Button _closeButton;
        private IPlayerPopupPresenter _playerPopupPresenter;
        private CompositeDisposable _compositeDisposable;

        [Inject]
        public void Construct(UserInfoView userInfoView,
            CharacterInfoView characterInfoView,
            PlayerLevelView playerLevelView,
            Button closeButton
            )
        {
            _userInfoView = userInfoView;
            _characterInfoView = characterInfoView;
            _playerLevelView = playerLevelView;
            _closeButton = closeButton;
        }

        public void Show(IPresenter args)
        {
            if (args is not IPlayerPopupPresenter playerPopupPresenter)
            {
                throw new ArgumentException("Expected IPlayerPopupPresenter");
            }
            gameObject.SetActive(true);
            _playerPopupPresenter = playerPopupPresenter;
            
            _compositeDisposable = new CompositeDisposable();
            ShowUserInfo(_playerPopupPresenter.UserInfoPresenter);
            ShowLevelInfo(_playerPopupPresenter.PlayerLevelPresenter);
            ShowStats(_playerPopupPresenter.CharacterInfoPresenter);
            _playerPopupPresenter.CloseCommand.Subscribe(OnCloseCommand).AddTo(_compositeDisposable);
            _playerPopupPresenter.CloseCommand.BindTo(_closeButton).AddTo(_compositeDisposable);
        }

        public void Close()
        {
            _playerPopupPresenter.CloseCommand.Execute();
        }

        private void ShowUserInfo(IUserInfoPresenter userInfoPresenter)
        {
            _userInfoView.Show(userInfoPresenter);
        }

        private void ShowLevelInfo(IPlayerLevelPresenter playerLevelPresenter)
        {
            _playerLevelView.Show(playerLevelPresenter);
        }

        private void ShowStats(ICharacterInfoPresenter characterInfoPresenter)
        {
            _characterInfoView.Show(characterInfoPresenter);
        }

        private void OnCloseCommand(Unit _)
        {
            _compositeDisposable.Dispose();
            Destroy(this.gameObject);
        }
    }
}
