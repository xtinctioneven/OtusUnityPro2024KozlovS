using UI;
using UnityEngine;

public class GameOverCheckTaskOld: EventTask
{
    private EventBus _eventBus;
    private HeroTrackerService _heroTrackerService;
    private TurnPipeline _turnPipeline;
    
    public GameOverCheckTaskOld(EventBus eventBus, HeroTrackerService heroTrackerService, TurnPipeline turnPipeline)
    {
        _eventBus = eventBus;
        _heroTrackerService = heroTrackerService;
        _turnPipeline = turnPipeline;
    }

    protected override void OnRun()
    {
        bool didBluePlayerLose = _heroTrackerService.GetBlueTeam().Count <= 0;
        bool didRedPlayerLose = _heroTrackerService.GetRedTeam().Count <= 0;
        if (didBluePlayerLose || didRedPlayerLose)
        {
            //_turnPipeline.ClearAll();
            // GameOverReason gameOverReason = new GameOverReason();
            // if (didBluePlayerLose && didRedPlayerLose)
            // {
            //     gameOverReason = GameOverReason.Draw;
            // }
            // else if (didBluePlayerLose)
            // {
            //     gameOverReason = GameOverReason.RedPlayerWin;
            // }
            // else
            // {
            //     gameOverReason = GameOverReason.BluePlayerWin;
            // }
            // _eventBus.RaiseEvent(new GameOverEvent(gameOverReason));
        }
        Finish();
    }

    public enum GameOverReason
    {
        RedPlayerWin = 1,
        BluePlayerWin = 2,
        Draw = 3
    }
}