using TMPro;
using UI;
using UnityEngine;

public class GameOverVisualHandler: BaseHandler<GameOverEvent>
{
    private readonly UIService _uiService;
    
    public GameOverVisualHandler(EventBus eventBus, UIService uiService) : base(eventBus)
    {
        _uiService = uiService;
    }

    protected override void OnHandleEvent(GameOverEvent evt)
    {
        string gameOverText ="Game Over";
        switch (evt.GameOverReason)
        {
            case GameOverCheckTask.GameOverReason.Draw:
            {
                gameOverText = "Game over!\nDraw!";
                break;
            }
            case GameOverCheckTask.GameOverReason.BluePlayerWin:
            {
                gameOverText = "Game over!\n<color=blue>Blue player wins!</color>";
                break;
            }
            case GameOverCheckTask.GameOverReason.RedPlayerWin:
            {
                gameOverText = "Game over!\n<color=red>Red player wins!</color>";
                break;
            }
            default:
            {
                Debug.LogError("Game over reason unknown!");
                break;
            }
        }

        GameOverPopup gameOverPopup = _uiService.GetGameOverPopup();
        gameOverPopup.SetText(gameOverText);
        gameOverPopup.SetActive(true);
    }
}