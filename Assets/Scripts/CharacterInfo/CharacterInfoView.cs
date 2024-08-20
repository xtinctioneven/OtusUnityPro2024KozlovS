using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class CharacterInfoView : MonoBehaviour
    {
        private CharacterStatView _characterStatViewPrefab;
        private Transform _statsViewContainer;
        private readonly List<CharacterStatView> _characterStatViews = new List<CharacterStatView>();
        private ICharacterInfoPresenter _characterInfoPresenter;
        private CompositeDisposable _compositeDisposable;
        private DiContainer _container;
        
        [Inject]
        public void Construct(CharacterStatView characterStatViewPrefab,
            Transform statsViewContainer,
            DiContainer container)
        {
            _characterStatViewPrefab = characterStatViewPrefab;
            _statsViewContainer = statsViewContainer;
            _container = container;
        }
        
        public void Show(IPresenter args)
        {
            if (args is not ICharacterInfoPresenter characterInfoPresenter)
            {
                throw new Exception("Expected ICharacterInfoPresenter!");
            }
            gameObject.SetActive(true);
            _characterInfoPresenter = characterInfoPresenter;
            
            _compositeDisposable = new CompositeDisposable();
            _characterInfoPresenter.CharacterStatsPresenters.ObserveAdd()
                .Subscribe(x => CreateCharacterStatView(x.Value)).AddTo(_compositeDisposable);
            _characterInfoPresenter.CharacterStatsPresenters.ObserveRemove()
                .Subscribe(x => RemoveCharacterStatView(x.Value)).AddTo(_compositeDisposable);
            _characterInfoPresenter.CloseCommand.Subscribe(OnCloseCommand).AddTo(_compositeDisposable);
            
            for (int i = 0; i < _characterInfoPresenter.CharacterStatsPresenters.Count; i++)
            {
                ICharacterStatPresenter characterStatPresenter = _characterInfoPresenter.CharacterStatsPresenters[i];
                CreateCharacterStatView(characterStatPresenter);
            }
        }

        private void CreateCharacterStatView(ICharacterStatPresenter characterStatPresenter)
        {
            CharacterStatView statView = _container.InstantiatePrefab(_characterStatViewPrefab, _statsViewContainer).GetComponent<CharacterStatView>();
            statView.Show(characterStatPresenter);
            statView.name = $"{_characterStatViewPrefab.name} {characterStatPresenter.Name}";
            _characterStatViews.Add(statView);
        }

        private void RemoveCharacterStatView(ICharacterStatPresenter characterStatPresenter)
        {
            for (int i = 0; i < _characterStatViews.Count; i++)
            {
                if (_characterStatViews[i].CharacterStatPresenter == characterStatPresenter)
                {
                    _characterStatViews.RemoveAt(i);
                    return;
                }
            }
        }

        private void OnCloseCommand(Unit _)
        {
            _compositeDisposable.Dispose();
            _characterStatViews.Clear();
            Destroy(this.gameObject);
        }
    }
}