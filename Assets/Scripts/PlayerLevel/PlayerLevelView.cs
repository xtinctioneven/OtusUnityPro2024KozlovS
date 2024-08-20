using DG.Tweening;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PlayerLevelView : MonoBehaviour
    {
        private Image _fillImage;
        private TMP_Text _levelText;
        private TMP_Text _XPText;
        private Button _addXPButton;
        private Button _levelUpButton;
        private IPlayerLevelPresenter _playerLevelPresenter;
        private CompositeDisposable _compositeDisposable = new();

        private readonly TweenChain _fillTweenChain = new();
        private readonly TweenChain _XPTweenChain = new();
        private const float FILL_SPEED = 0.3f;
        private const float TEXT_SPEED = 0.3f;

        [Inject]
        private void Construct(Image fillImage, 
            [Inject(Id = PlayerLevelInstaller.LEVEL_TEXT_ID)] TMP_Text levelText, 
            [Inject(Id = PlayerLevelInstaller.XP_TEXT_ID)] TMP_Text XPText, 
            [Inject(Id = PlayerLevelInstaller.XP_BUTTON_ID)] Button addXPButton,
            [Inject(Id = PlayerLevelInstaller.LEVEL_BUTTON_ID)] Button levelUpButton)
        {
            _fillImage = fillImage;
            _levelText = levelText;
            _XPText = XPText;
            _addXPButton = addXPButton;
            _levelUpButton = levelUpButton;
        }

        public void Show(IPresenter args)
        {
            if (args is not IPlayerLevelPresenter playerLevelPresenter)
            {
                throw new ArgumentException("Expected IPlayerLevelPresenter");
            }
            _playerLevelPresenter = playerLevelPresenter;
            gameObject.SetActive(true);
            _compositeDisposable.Dispose();
            _compositeDisposable = new CompositeDisposable();

            _levelText.text = $"Level {_playerLevelPresenter.CurrentLevel}";
            _XPText.text = $"XP {_playerLevelPresenter.CurrentExperience}/{_playerLevelPresenter.RequiredExperience}";
            _fillImage.fillAmount = float.Parse(_playerLevelPresenter.CurrentExperience.Value) 
                                    / float.Parse(_playerLevelPresenter.RequiredExperience.Value);

            _playerLevelPresenter.LevelUpCommand.BindTo(_levelUpButton).AddTo(_compositeDisposable);
            _playerLevelPresenter.AddExperienceCommand.BindTo(_addXPButton).AddTo(_compositeDisposable);
            _playerLevelPresenter.CurrentExperience.Pairwise().Subscribe(UpdateCurrentExperience).AddTo(_compositeDisposable);
            _playerLevelPresenter.CurrentLevel.Subscribe(UpdateLevel).AddTo(_compositeDisposable);
            _playerLevelPresenter.RequiredExperience.Pairwise().Subscribe(UpdateRequiredExperience).AddTo(_compositeDisposable);
            _playerLevelPresenter.CloseCommand.Subscribe(OnCloseCommand).AddTo(_compositeDisposable);
        }

        private void UpdateCurrentExperience(Pair<string> values)
        {
            string requiredExperience = _playerLevelPresenter.RequiredExperience.Value;
            float fillAmount = float.Parse(values.Current) / float.Parse(requiredExperience);
            _fillTweenChain.AddAndPlay(_fillImage.DOFillAmount(fillAmount, FILL_SPEED).SetEase(Ease.Linear));
            _XPTweenChain.AddAndPlay(DOTween.To(() => int.Parse(values.Previous), 
                 CurrentExperienceTextSetter, int.Parse(values.Current), TEXT_SPEED).SetEase(Ease.Linear));
        }

        private void CurrentExperienceTextSetter(int text)
        {
            _XPText.text = $"XP {text}/{_playerLevelPresenter.RequiredExperience}";
        }

        private void UpdateRequiredExperience(Pair<string> values)
        {
            _XPTweenChain.AddAndPlay(DOTween.To(() => int.Parse(values.Previous), 
                RequiredExperienceTextSetter, int.Parse(values.Current), TEXT_SPEED).SetEase(Ease.Linear));
        }

        private void RequiredExperienceTextSetter(int text)
        {
            _XPText.text = $"XP {_playerLevelPresenter.CurrentExperience}/{text}";
        }

        private void UpdateLevel(string value)
        {
            _levelText.text = $"Level {value}";
        }

        private void OnCloseCommand(Unit _)
        {
            _compositeDisposable.Dispose();
            _fillTweenChain.Destroy();
            _XPTweenChain.Destroy();
            Destroy(this.gameObject);
        }
    }
}
