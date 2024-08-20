using UniRx;

namespace Lessons.Architecture.PM
{
    public class CharacterStatPresenter : ICharacterStatPresenter
    {
        public IReadOnlyReactiveProperty<string> Name => _name;
        private readonly ReactiveProperty<string> _name = new StringReactiveProperty();
        public IReadOnlyReactiveProperty<string> Value => _value;
        private readonly ReactiveProperty<string> _value = new StringReactiveProperty();
        public ReactiveCommand CloseCommand { get; }
        public CharacterStat CharacterStat => _characterStat;
        
        private readonly CharacterStat _characterStat;
        private readonly CompositeDisposable _compositeDisposable = new();
        
        public CharacterStatPresenter (CharacterStat characterStat, IPlayerLevelPresenter playerLevelPresenter)
        {
            _characterStat = characterStat;
            _name.Value = _characterStat.Name.Value;
            _value.Value = _characterStat.Value.ToString();

            CloseCommand = new ReactiveCommand();
            _characterStat.Name.Subscribe(CharacterStat_OnNameChanged).AddTo(_compositeDisposable);
            _characterStat.Value.Subscribe(CharacterStat_OnValueChanged).AddTo(_compositeDisposable);
            playerLevelPresenter.LevelUpCommand.Subscribe(OnLevelUpCommand).AddTo(_compositeDisposable);
            CloseCommand.Subscribe(OnExternalCloseCommand).AddTo(_compositeDisposable);
        }

        private void OnLevelUpCommand(Unit _)
        {
            ChangeValue(_characterStat.Value.Value + _characterStat.LevelModifier);
        }

        public void ChangeValue(int newValue)
        {
            _characterStat.ChangeValue(newValue);
        }

        private void CharacterStat_OnValueChanged(int newValue)
        {
            _value.Value = newValue.ToString();
        }

        public void ChangeName(string newName)
        {
            _characterStat.ChangeName(newName);
        }

        private void CharacterStat_OnNameChanged(string newName)
        {
            _name.Value = newName;
        }

        private void OnExternalCloseCommand(Unit _)
        {
            _compositeDisposable?.Dispose();
        }
    }
}
