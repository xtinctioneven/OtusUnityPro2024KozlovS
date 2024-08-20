using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PopupManager : MonoBehaviour
    {
        private PlayerPopupView _playerPopupPrefab;
        private Transform _popupContainer;
        private TMP_Dropdown _playerConfigDropdown;
        private Button _popupShowButton;
        
        [Header("Current popup presenter data (for tests)")]
        [SerializeField] private UserInfo _currentUserInfo;
        [SerializeField] private PlayerLevel _currentPlayerLevel;
        [SerializeField] private CharacterInfo _currentCharacterInfo;
        
        private PlayerPopupPresenterFactory _playerPopupPresenterFactory;
        private ConfigParser _configParser;
        private PlayerPopupView _playerPopup;
        private DiContainer _container;
        
        private List<UserInfo> _userInfos;
        private List<PlayerLevel> _playerLevels;
        private List<CharacterInfo> _characterInfos;
        
        [Inject]
        private void Construct(PlayerPopupView playerPopupPrefab,
            Transform popupContainer,
            TMP_Dropdown playerConfigDropdown,
            Button popupShowButton,
            PlayerPopupPresenterFactory playerPopupPresenterFactory,
            List<UserInfo> userInfos,
            List<PlayerLevel> playerLevels,
            List<CharacterInfo> characterInfos,
            DiContainer container)
        {
            _playerPopupPrefab = playerPopupPrefab;
            _popupContainer = popupContainer;
            _playerConfigDropdown = playerConfigDropdown;
            _popupShowButton = popupShowButton;
            _playerPopupPresenterFactory = playerPopupPresenterFactory;
            _userInfos = userInfos;
            _playerLevels = playerLevels;
            _characterInfos = characterInfos;
            _container = container;
        }
        
        public void Start()
        {
            _playerConfigDropdown.ClearOptions();
            for (int i = 0; i < _userInfos.Count; i++)
            {
                UserInfo userInfo = _userInfos[i];
                _playerConfigDropdown.options.Add(new TMP_Dropdown.OptionData(
                    userInfo.Name.Value,
                    userInfo.Icon.Value)
                );
            }
            _popupShowButton.onClick.AddListener(ShowSelectedPopup);
        }

        private void ShowSelectedPopup()
        {
           _playerPopup?.Close();
            if (_playerConfigDropdown.options.Count < 1)
            {
                throw new ArgumentException("Nothing to show!");
                return;
            }
            int index = _playerConfigDropdown.value;
            _currentUserInfo = _userInfos[index];
            _currentPlayerLevel = _playerLevels[index];
            _currentCharacterInfo = _characterInfos[index];
            _playerPopup = _container.InstantiatePrefab(_playerPopupPrefab, _popupContainer).GetComponent<PlayerPopupView>();
            IPlayerPopupPresenter playerPopupPresenter = _playerPopupPresenterFactory.Create(
                _userInfos[index],
                _playerLevels[index],
                _characterInfos[index]
                );
            _playerPopup.name = $"{_playerPopupPrefab.name} {playerPopupPresenter.UserInfoPresenter.Name}";
            _playerPopup.Show(playerPopupPresenter);
        }
    }
}
