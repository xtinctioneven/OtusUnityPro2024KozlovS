using System;
using UniRx;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public class CharacterInfoPresenter : ICharacterInfoPresenter
    {
        public IReadOnlyReactiveCollection<ICharacterStatPresenter> CharacterStatsPresenters => _characterStatPresenters;
        public ReactiveCommand CloseCommand { get; }
        private readonly ReactiveCollection<ICharacterStatPresenter> _characterStatPresenters = new ReactiveCollection<ICharacterStatPresenter>();
        private readonly CharacterStatPresenterFactory _characterStatPresenterFactory;
        private readonly IPlayerLevelPresenter _playerLevelPresenter;
        private readonly CharacterInfo _characterInfo;
        private readonly CompositeDisposable _compositeDisposable;

        public CharacterInfoPresenter(CharacterInfo characterInfo, CharacterStatPresenterFactory characterStatPresenterFactory, 
            IPlayerLevelPresenter playerLevelPresenter)
        {
            _characterInfo = characterInfo;
            _characterStatPresenterFactory = characterStatPresenterFactory;
            _playerLevelPresenter = playerLevelPresenter;
            
            _compositeDisposable = new CompositeDisposable();
            _characterInfo.CharacterStats.ObserveAdd().Subscribe(x => CharacterInfo_OnStatAdded(x.Value)).AddTo(_compositeDisposable);
            _characterInfo.CharacterStats.ObserveRemove().Subscribe(x => CharacterInfo_OnStatRemoved(x.Value)).AddTo(_compositeDisposable);
            CloseCommand = new ReactiveCommand();
            CloseCommand.Subscribe(OnExternalCloseCommand).AddTo(_compositeDisposable);
            CharacterStat[] stats = _characterInfo.GetStats();
            for (int i = 0; i < stats.Length; i++)
            {
                CreateCharacterStatPresenter(stats[i]);
            }
        }
        
        private void CharacterInfo_OnStatAdded(CharacterStat characterStat)
        {
            CreateCharacterStatPresenter(characterStat);
        }
        
        private void CharacterInfo_OnStatRemoved(CharacterStat characterStat)
        {
            RemoveStatPresenter(characterStat);
        }

        private void CreateCharacterStatPresenter(CharacterStat characterStat)
        {
            ICharacterStatPresenter characterStatPresenter = _characterStatPresenterFactory.Create(characterStat, _playerLevelPresenter);
            _characterStatPresenters.Add(characterStatPresenter);
            CloseCommand.Subscribe(x => characterStatPresenter.CloseCommand.Execute()).AddTo(_compositeDisposable);
        }

        private void RemoveStatPresenter(CharacterStat characterStat)
        {
            for (int i = 0; i < _characterStatPresenters.Count; i++)
            {
                if (_characterStatPresenters[i].CharacterStat == characterStat)
                {
                    _characterStatPresenters[i].CloseCommand.Execute();
                    _characterStatPresenters.RemoveAt(i);
                }
            }
        }

        private void OnExternalCloseCommand(Unit _)
        {
            _compositeDisposable.Dispose();
        }
    }
}
