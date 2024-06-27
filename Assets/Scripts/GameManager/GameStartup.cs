using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class GameStartup : MonoBehaviour, IGameStartListener
{
    [Header("References")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Button _startButton;
    [SerializeField] private Image _backgroundFade;
    [SerializeField] private TextMeshProUGUI _startCountdownText;
    [Header("Countdown Settings")]
    [SerializeField] private float _fadeInAlphaStep;
    [SerializeField] private int _countdownTime;
    [SerializeField] private float _TextStartRotation;
    [SerializeField] private float _TextEndRotation;
    [SerializeField] private float _TextStartScale;
    [SerializeField] private float _TextEndScale;
    private Timer _countdownTimer;

    private void Awake()
    {
        UpdateCountdownText();
    }

    private void Start()
    {
        IGameListener.Register(this);
    }

    public void StartGame()
    {
        _gameManager.StartGame();
    }

    public void OnGameStart()
    {
        _countdownTimer = new Timer(1f);
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        _startButton.gameObject.SetActive(false);
        _startCountdownText.gameObject.SetActive(true);
        float fadeInStepTime = _countdownTime / (1f/_fadeInAlphaStep);
        float textRotationStep = ( _TextEndRotation - _TextStartRotation) / (1f/fadeInStepTime);
        float textScaleStep = ( _TextEndScale - _TextStartScale) / (1f / fadeInStepTime);
        ResetCountdownTextTransform();
        for (float fadeAlpha = 1f; fadeAlpha > 0; fadeAlpha -= _fadeInAlphaStep)
        {
            Color fadeColor = _backgroundFade.color;
            fadeColor.a = fadeAlpha;
            _backgroundFade.color = fadeColor;
            yield return new WaitForSeconds(fadeInStepTime);

            UpdateCountdownTextTransform(textRotationStep, textScaleStep);

            if (_countdownTimer.Tick(fadeInStepTime))
            {
                ResetCountdownTextTransform();
                _countdownTime--;
                UpdateCountdownText();
            }
        }
        _backgroundFade.gameObject.SetActive(false);
        _startCountdownText.gameObject.SetActive(false);
        _gameManager.PlayGame();
    }

    private void UpdateCountdownTextTransform(float textRotationStep, float textScaleStep)
    {
        Transform transform = _startCountdownText.transform;
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
        Transform transform = _startCountdownText.transform;
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
        _startCountdownText.text = _countdownTime.ToString() + "!";
    }
}
