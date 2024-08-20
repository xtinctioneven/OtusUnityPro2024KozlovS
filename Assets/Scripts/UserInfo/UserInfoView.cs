using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class UserInfoView : MonoBehaviour
    {
        private TMP_Text _nameText;
        private TMP_Text _descriptionText;
        private Image _avatar;
        private IUserInfoPresenter _userInfoPresenter;
        private CompositeDisposable _compositeDisposable;

        [Inject]
        public void Construct(
            [Inject(Id = UserInfoInstaller.NAME_TEXT_ID)]
            TMP_Text nameText,
            [Inject(Id = UserInfoInstaller.DESCRIPTION_TEXT_ID)]
            TMP_Text descriptionText,
            Image avatar)
        {
            _nameText = nameText;
            _descriptionText = descriptionText;
            _avatar = avatar;
        }
        
        public void Show(IPresenter args)
        {
            if (args is not IUserInfoPresenter userInfoPresenter)
            {
                throw new ArgumentException("Expected IUserInfoPresenter!");
            }
            gameObject.SetActive(true);
            _userInfoPresenter = userInfoPresenter;
            _nameText.text = _userInfoPresenter.Name.Value;
            _descriptionText.text = _userInfoPresenter.Description.Value;
            _avatar.sprite = _userInfoPresenter.Icon.Value;
            
            _compositeDisposable = new CompositeDisposable();
            _userInfoPresenter.Name.Subscribe(OnChangeName).AddTo(_compositeDisposable);
            _userInfoPresenter.Description.Subscribe(OnChangeDescription).AddTo(_compositeDisposable);
            _userInfoPresenter.Icon.Subscribe(OnChangeIcon).AddTo(_compositeDisposable);
            _userInfoPresenter.CloseCommand.Subscribe(OnCloseCommand).AddTo(_compositeDisposable);
        }

        private void OnChangeName(string name)
        {
            _nameText.text = name;
        }

        private void OnChangeDescription(string description)
        {
            _descriptionText.text = description;
        }

        private void OnChangeIcon(Sprite icon)
        {
            _avatar.sprite = icon;
        }

        private void OnCloseCommand(Unit _)
        {
            _compositeDisposable.Dispose();
            Destroy(this.gameObject);
        }
    }
}
