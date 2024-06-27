using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseManager : MonoBehaviour, IGamePlayListener, 
    IGamePauseListener, IGameFinishListener
{
    public delegate void TogglePauseDelegate();
    public TogglePauseDelegate TogglePause;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Image _playButtonVisual;


    private void Start()
    {
        IGameListener.Register(this);
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
        enabled = false;
    }

    public void ButtonClick()
    {
        TogglePause?.Invoke();
    }


}
