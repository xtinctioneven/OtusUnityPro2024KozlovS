using UniRx;

namespace Lessons.Architecture.PM
{
    public interface ICharacterInfoPresenter : IPresenter
    {
        IReadOnlyReactiveCollection<ICharacterStatPresenter> CharacterStatsPresenters { get; }
        ReactiveCommand CloseCommand { get; }
    }
}
