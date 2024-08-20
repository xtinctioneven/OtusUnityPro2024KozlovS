using Zenject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public class PopupManagerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerPopupView _playerPopupPrefab;
        [SerializeField] private Transform _popupContainer;
        [SerializeField] private TMP_Dropdown _playerConfigDropdown;
        [SerializeField] private Button _popupShowButton;
        public override void InstallBindings()
        {
            Container.Bind<PlayerPopupView>().FromInstance(_playerPopupPrefab).AsSingle().NonLazy();
            Container.Bind<Transform>().FromInstance(_popupContainer).AsSingle().NonLazy();
            Container.Bind<TMP_Dropdown>().FromInstance(_playerConfigDropdown).AsSingle().NonLazy();
            Container.Bind<Button>().FromInstance(_popupShowButton).AsSingle().NonLazy();
        }
    }
}