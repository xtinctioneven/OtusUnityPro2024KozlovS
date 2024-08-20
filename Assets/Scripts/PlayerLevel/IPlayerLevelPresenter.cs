using UniRx;

namespace Lessons.Architecture.PM
{
    public interface IPlayerLevelPresenter : IPresenter
    {
        public IReadOnlyReactiveProperty<string> CurrentLevel { get; }
        public IReadOnlyReactiveProperty<string> CurrentExperience { get; }
        public IReadOnlyReactiveProperty<string> RequiredExperience { get; }
        public IReadOnlyReactiveProperty<bool> CanLevelUp { get; }
        public ReactiveCommand AddExperienceCommand { get; }
        public ReactiveCommand LevelUpCommand { get; }
        ReactiveCommand CloseCommand { get; }
        public void AddExperience(int amount);
        public void LevelUp();
    }
}