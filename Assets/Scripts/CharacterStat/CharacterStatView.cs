using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;
using Unit = UniRx.Unit;

namespace Lessons.Architecture.PM
{
    public class CharacterStatView : MonoBehaviour
    {
        private TMP_Text _statName;
        private TMP_Text _statValue;
        public ICharacterStatPresenter CharacterStatPresenter => _characterStatPresenter;
        private ICharacterStatPresenter _characterStatPresenter;
        private readonly CompositeDisposable _compositeDisposable = new();
        private Tweener _valueTweener;

        [Inject]
        public void Construct(
            [Inject(Id = CharacterStatInstaller.STAT_NAME_ID)] TMP_Text statName, 
            [Inject(Id = CharacterStatInstaller.STAT_VALUE_ID)] TMP_Text statValue)
        {
            _statName = statName;
            _statValue = statValue;
        }
        
        public void Show(IPresenter presenter)
        {
            if (presenter is not ICharacterStatPresenter characterStatPresenter)
            {
                throw new System.ArgumentException("Expected CharacterStatPresenter");
            }
            gameObject.SetActive(true);
            _characterStatPresenter = characterStatPresenter;
            _statName.text = _characterStatPresenter.Name.Value;
            _statValue.text = _characterStatPresenter.Value.Value;

            _characterStatPresenter.Name.Subscribe(OnNameChange).AddTo(_compositeDisposable);
            _characterStatPresenter.Value.Pairwise().Subscribe(OnValueChange).AddTo(_compositeDisposable);
            _characterStatPresenter.CloseCommand.Subscribe(OnCloseCommand).AddTo(_compositeDisposable);
        }

        private void OnNameChange(string newName)
        {
            _statName.text = newName;
        }

        private void OnValueChange(Pair<string> values)
        {
            _valueTweener = DOTween.To(() => int.Parse(values.Previous), StatTextSetter, int.Parse(values.Current), .6f);
        }

        private void StatTextSetter(int newValue)
        {
            _statValue.text = newValue.ToString();
        }

        private void OnCloseCommand(Unit _)
        {
            _valueTweener.Kill();
            _compositeDisposable.Dispose();
            Destroy(this.gameObject);
        }
    }
}