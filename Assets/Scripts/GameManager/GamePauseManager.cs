using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public class GamePauseManager : IInitializable, IGamePlayListener,
    IGamePauseListener, IGameFinishListener
    {
        public delegate void TogglePauseDelegate();
        public TogglePauseDelegate TogglePause;
        private GameManager _gameManager;
        private Image _playButtonVisual;
        private Button _pauseButton;

        [Inject]
        private void Construct(GameManager gameManager, Image playButtonVisual, Button pauseButton)
        {
            _gameManager = gameManager;
            _playButtonVisual = playButtonVisual;
            _pauseButton = pauseButton;
        }

        public void Initialize()
        {
            IGameListener.Register(this);
            _pauseButton.onClick.AddListener(TogglePauseClick);
        }

        public void OnGamePause()
        {
            TogglePause = _gameManager.PlayGame;
            _playButtonVisual.gameObject.SetActive(true);
        }

        public void OnGamePlay()
        {
            TogglePause = _gameManager.PauseGame;
            _playButtonVisual.gameObject.SetActive(false);
        }

        public void OnGameFinish()
        {
            _pauseButton.gameObject.SetActive(false);
        }

        public void TogglePauseClick()
        {
            TogglePause?.Invoke();
        }
    }
}
