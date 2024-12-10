using Zenject;

public class GameOverCheckTask: EventTask
{
    private EventBus _eventBus;
    readonly EntityTrackerService _entityTrackerService;
    
    public GameOverCheckTask(EventBus eventBus, EntityTrackerService entityTrackerService)
    {
        _eventBus = eventBus;
        _entityTrackerService = entityTrackerService;
    }

    protected override void OnRun()
    {
        bool isLeftPlayerLose = _entityTrackerService.GetLeftTeam().Count <= 0;
        bool isRightPlayerLose = _entityTrackerService.GetRightTeam().Count <= 0;
        if (isLeftPlayerLose || isRightPlayerLose)
        {
            //TODO: !!!
            //_turnPipeline.ClearAll();
            //GameOverReason gameOverReason = new GameOverReason();
            // if (isLeftPlayerLose && isRightPlayerLose)
            // {
            //     gameOverReason = GameOverReason.Draw;
            // }
            // else if (isLeftPlayerLose)
            // {
            //     gameOverReason = GameOverReason.RedPlayerWin;
            // }
            // else
            // {
            //     gameOverReason = GameOverReason.BluePlayerWin;
            // }
            // _eventBus.RaiseEvent(new GameOverEvent(gameOverReason));
            Helper.Instance.IsGameOver = true;
            Helper.Instance.AddLog("Game Over!");
        }
        Finish();
    }

    public enum GameOverReason
    {
        LeftTeamWin = 1,
        RightTeamWin = 2,
        Draw = 3
    }
}