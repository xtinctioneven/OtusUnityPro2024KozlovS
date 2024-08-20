using UniRx;

namespace Lessons.Architecture.PM
{
    public interface ICharacterStatPresenter : IPresenter
    {
        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<string> Value { get; }
        ReactiveCommand CloseCommand { get; }
        CharacterStat CharacterStat { get; }
        void ChangeValue(int newValue);
        void ChangeName(string newName);
    }
}
