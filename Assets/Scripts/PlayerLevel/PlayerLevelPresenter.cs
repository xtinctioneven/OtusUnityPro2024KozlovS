using UniRx;

namespace Lessons.Architecture.PM
{
    public class PlayerLevelPresenter : IPlayerLevelPresenter
    {
        private const int ADD_EXPERIENCE_AMOUNT = 100;
        private readonly CompositeDisposable _compositeDisposable;
        public ReactiveCommand LevelUpCommand { get; }
        public ReactiveCommand CloseCommand { get; }
        public ReactiveCommand AddExperienceCommand { get; }

        public IReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;
        private readonly ReactiveProperty<bool> _canLevelUp = new BoolReactiveProperty();
        public IReadOnlyReactiveProperty<string> CurrentExperience => _currentExperience;
        private readonly ReactiveProperty<string> _currentExperience = new StringReactiveProperty();
        public IReadOnlyReactiveProperty<string> RequiredExperience => _requiredExperience;
        private readonly ReactiveProperty<string> _requiredExperience = new StringReactiveProperty();
        public IReadOnlyReactiveProperty<string> CurrentLevel => _currentLevel;
        private readonly ReactiveProperty<string> _currentLevel = new StringReactiveProperty();

        private readonly PlayerLevel _playerLevel;

        public PlayerLevelPresenter(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;

            _currentLevel.Value = _playerLevel.CurrentLevel.ToString();
            _requiredExperience.Value = _playerLevel.RequiredExperience.ToString();
            _currentExperience.Value = _playerLevel.CurrentExperience.ToString();
            
            _compositeDisposable = new CompositeDisposable();
            _playerLevel.CurrentLevel.Subscribe(PlayerLevel_OnLevelUp).AddTo(_compositeDisposable);
            _playerLevel.CurrentExperience.Subscribe(PlayerLevel_OnExperienceChanged).AddTo(_compositeDisposable);
            CurrentExperience.Subscribe(UpdateCanLevelUp).AddTo(_compositeDisposable);
            AddExperienceCommand = new ReactiveCommand();
            AddExperienceCommand.Subscribe(OnAddExperienceCommand).AddTo(_compositeDisposable);
            LevelUpCommand = new ReactiveCommand(CanLevelUp);
            LevelUpCommand.Subscribe(OnLevelUpCommand).AddTo(_compositeDisposable);
            CloseCommand = new ReactiveCommand();
            CloseCommand.Subscribe(OnExternalCloseCommand).AddTo(_compositeDisposable);
        }

        private void OnAddExperienceCommand(Unit _)
        {
            AddExperience(ADD_EXPERIENCE_AMOUNT);
        }

        public void AddExperience(int amount)
        {
            _playerLevel.AddExperience(amount);
        }

        private void PlayerLevel_OnExperienceChanged(int currentExperience)
        {
            _currentExperience.Value = currentExperience.ToString();
        }

        private void OnLevelUpCommand(Unit _)
        {
            LevelUp();
        }

        public void LevelUp()
        {
            _playerLevel.LevelUp();
        }

        private void PlayerLevel_OnLevelUp(int currentLevel)
        {
            _currentLevel.Value = currentLevel.ToString();
            _requiredExperience.Value = _playerLevel.RequiredExperience.ToString();
        }

        private void UpdateCanLevelUp(string _)
        {
            _canLevelUp.Value = _playerLevel.CanLevelUp();
        }

        private void OnExternalCloseCommand(Unit _)
        {
            _compositeDisposable.Dispose();
        }
    }
}