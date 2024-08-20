using UniRx;

namespace Lessons.Architecture.PM
{
    public interface IPlayerPopupPresenter : IPresenter
    {
        IUserInfoPresenter UserInfoPresenter { get; }
        ICharacterInfoPresenter CharacterInfoPresenter { get; }
        IPlayerLevelPresenter PlayerLevelPresenter { get; }
        ReactiveCommand CloseCommand { get; }
    }
}