using UniRx;

namespace Lessons.Architecture.PM
{
    public class PlayerPopupPresenter : IPlayerPopupPresenter
    {
        public IUserInfoPresenter UserInfoPresenter => _userInfoPresenter;
        public ICharacterInfoPresenter CharacterInfoPresenter => _characterInfoPresenter;
        public IPlayerLevelPresenter PlayerLevelPresenter => _playerLevelPresenter;
        public ReactiveCommand CloseCommand { get; }
        
        private readonly IUserInfoPresenter _userInfoPresenter;
        private readonly ICharacterInfoPresenter _characterInfoPresenter;
        private readonly IPlayerLevelPresenter _playerLevelPresenter;
        private readonly CompositeDisposable _compositeDisposable;

        public PlayerPopupPresenter(UserInfo userInfo, PlayerLevel playerLevel,
            CharacterInfo characterInfo, CharacterStatPresenterFactory characterStatPresenterFactory)
        {
            _userInfoPresenter = new UserInfoPresenter(userInfo);
            _playerLevelPresenter = new PlayerLevelPresenter(playerLevel);
            _characterInfoPresenter = new CharacterInfoPresenter(characterInfo, characterStatPresenterFactory, _playerLevelPresenter);

            _compositeDisposable = new CompositeDisposable();
            CloseCommand = new ReactiveCommand();
            CloseCommand.Subscribe(x => _userInfoPresenter.CloseCommand.Execute()).AddTo(_compositeDisposable);
            CloseCommand.Subscribe(x => _playerLevelPresenter.CloseCommand.Execute()).AddTo(_compositeDisposable);
            CloseCommand.Subscribe(x => _characterInfoPresenter.CloseCommand.Execute()).AddTo(_compositeDisposable);
        }

        public void Close()
        {
            _compositeDisposable.Dispose();
            CloseCommand.Execute();
        }
    }
}