using UnityEngine;
using Zenject;

public class GameOverPopup 
{
    private GameObject _gameOverPopup;
    private PlayerDeathObserver _playerDeathObserver;
    
    [Inject]
    private void Construct(PlayerDeathObserver playerDeathObserver, 
        [Inject(Id = SceneInstaller.GAME_OVER_POPUP_NAME)] GameObject gameOverPopup)
    {
        _gameOverPopup = gameOverPopup;
        _playerDeathObserver = playerDeathObserver;
        _playerDeathObserver.GameOver.Subscribe(OnGameOver);
    }

    private void OnGameOver()
    {
        _gameOverPopup.SetActive(true);
    }
}