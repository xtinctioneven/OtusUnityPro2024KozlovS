using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public class GameStartup : IGameStartListener, IGameUpdateListener, IInitializable, IDisposable
    {
        //Countdown initital settings
        private float _fadeInAlphaStep;
        private int _countdownTime;
        private float _TextStartRotation;
        private float _TextEndRotation;
        private float _TextStartScale;
        private float _TextEndScale;
        
        //Countdown derivative settings
        private Timer _countdownTimer;
        private Timer _fadeTimer;
        private float _fadeInStepTime;
        private float _textRotationStep;
        private float _textScaleStep;
        private float _fadeAlpha = 1f;

        //References
        private GameManager _gameManager;
        private Button _startButton;
        private Image _backgroundFade;
        private TextMeshProUGUI _countdownText;

        [Inject]
        private void Construct(
            GameManager gameManager, 
            Button startButton, 
            Image backgroundFade, 
            TextMeshProUGUI countdownText,
            GameStartupSettings gameStartupSettings
            )
        {
            _gameManager = gameManager;
            _startButton = startButton;
            _backgroundFade = backgroundFade;
            _countdownText = countdownText;

            _fadeInAlphaStep = gameStartupSettings.FadeInAlphaStep;
            _countdownTime = gameStartupSettings.CountdownTime;
            _TextStartRotation = gameStartupSettings.TextStartRotation;
            _TextEndRotation = gameStartupSettings.TextEndRotation;
            _TextStartScale = gameStartupSettings.TextStartScale;
            _TextEndScale = gameStartupSettings.TextEndScale;
        }

        public void Initialize()
        {
            UpdateCountdownText();
            IGameListener.Register(this);
            _startButton.onClick.AddListener(StartGame);
        }

        public void StartGame()
        {
            _gameManager.StartGame();
        }

        public void OnGameStart()
        {
            StartCountdown();
        }

        public void OnGameUpdate(float deltaTime)
        {
            if (_fadeTimer.Tick(Time.deltaTime))
            {
                Color fadeColor = _backgroundFade.color;
                _fadeAlpha -= _fadeInAlphaStep;
                fadeColor.a = _fadeAlpha;
                _backgroundFade.color = fadeColor;
                UpdateCountdownTextTransform(_textRotationStep, _textScaleStep);
            }

            if (_countdownTimer.Tick(Time.deltaTime))
            {
                ResetCountdownTextTransform();
                _countdownTime--;
                if (_countdownTime <= 0)
                {
                    FinishCountdown();
                }
                else
                {
                    UpdateCountdownText();
                }
            }
        }

        private void StartCountdown()
        {
            _startButton.gameObject.SetActive(false);
            _countdownText.gameObject.SetActive(true);
            _fadeInStepTime = _countdownTime / (1f / _fadeInAlphaStep);
            _textRotationStep = (_TextEndRotation - _TextStartRotation) / (1f / _fadeInStepTime);
            _textScaleStep = (_TextEndScale - _TextStartScale) / (1f / _fadeInStepTime);
            _countdownTimer = new Timer(1f);
            _fadeTimer = new Timer(_fadeInStepTime);
            ResetCountdownTextTransform();
            _fadeAlpha = 1f;
        }

        private void FinishCountdown()
        {
            _backgroundFade.gameObject.SetActive(false);
            _countdownText.gameObject.SetActive(false);
            _gameManager.PlayGame();
            Dispose();
        }

        private void UpdateCountdownTextTransform(float textRotationStep, float textScaleStep)
        {
            Transform transform = _countdownText.transform;
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.z += textRotationStep;
            Vector3 scale = transform.localScale;
            scale.x += textScaleStep;
            scale.y += textScaleStep;
            transform.eulerAngles = rotation;
            transform.localScale = scale;
        }

        private void ResetCountdownTextTransform()
        {
            Transform transform = _countdownText.transform;
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.z = _TextStartRotation;
            Vector3 scale = transform.localScale;
            scale.x = _TextStartScale;
            scale.y = _TextStartScale;
            transform.eulerAngles = rotation;
            transform.localScale = scale;
        }

        private void UpdateCountdownText()
        {
            _countdownText.text = _countdownTime.ToString() + "!";
        }

        public void Dispose()
        {
            IGameListener.Deregister(this);
        }
    }

    [Serializable]
    public struct GameStartupSettings
    {
        public float FadeInAlphaStep;
        public int CountdownTime;
        public float TextStartRotation;
        public float TextEndRotation;
        public float TextStartScale;
        public float TextEndScale;
    }
}