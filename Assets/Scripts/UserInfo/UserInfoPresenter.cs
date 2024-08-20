using UniRx;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public class UserInfoPresenter : IUserInfoPresenter
    {
        public IReadOnlyReactiveProperty<string> Name => _name;
        private readonly ReactiveProperty<string> _name = new StringReactiveProperty();
        public IReadOnlyReactiveProperty<string> Description => _description;
        private readonly ReactiveProperty<string> _description = new StringReactiveProperty();
        public IReadOnlyReactiveProperty<Sprite> Icon => _icon;
        public ReactiveCommand CloseCommand { get; }
        private readonly ReactiveProperty<Sprite> _icon = new ReactiveProperty<Sprite>();
        
        private readonly CompositeDisposable _compositeDisposable;
        private readonly UserInfo _userInfo;

        public UserInfoPresenter(UserInfo userInfo)
        {
            _userInfo = userInfo;
            _name.Value = _userInfo.Name.Value;
            _description.Value = _userInfo.Description.Value;
            _icon.Value = _userInfo.Icon.Value;
            
            _compositeDisposable = new CompositeDisposable();
            _userInfo.Name.Subscribe(UserInfo_OnChangeName).AddTo(_compositeDisposable);
            _userInfo.Description.Subscribe(UserInfo_OnChangeDescription).AddTo(_compositeDisposable);
            _userInfo.Icon.Subscribe(UserInfo_OnChangeIcon).AddTo(_compositeDisposable);
            CloseCommand = new ReactiveCommand();
            CloseCommand.Subscribe(OnExternalCloseCommand).AddTo(_compositeDisposable);
        }

        private void UserInfo_OnChangeName(string name)
        {
            _name.Value = name;
        }

        private void UserInfo_OnChangeDescription(string description)
        {
            _description.Value = description;
        }

        private void UserInfo_OnChangeIcon(Sprite icon)
        {
            _icon.Value = icon;
        }
        
        public void ChangeName(string name)
        {
            _userInfo.ChangeName(name);
        }

        public void ChangeDescription(string description)
        {
            _userInfo.ChangeDescription(description);
        }

        public void ChangeIcon(Sprite icon)
        {
            _userInfo.ChangeIcon(icon);
        }

        private void OnExternalCloseCommand(Unit _)
        {
            _compositeDisposable.Dispose();
        }
    }
}