using Zenject;

public class GameOverCheckTask: EventTask
{
    private EventBus _eventBus;
    private DiContainer _diContainer;
    //private HeroTrackerService _heroTrackerService;
    //private TurnPipeline _turnPipeline;
    
    public GameOverCheckTask(EventBus eventBus, DiContainer diContainer)
    {
        _eventBus = eventBus;
        // _heroTrackerService = heroTrackerService;
        // _turnPipeline = turnPipeline;
        _diContainer = diContainer;
    }

    protected override void OnRun()
    {
        EntityTrackerService trackerService = _diContainer.Resolve<EntityTrackerService>();
        bool isLeftPlayerLose = trackerService.GetLeftTeam().Count <= 0;
        bool isRightPlayerLose = trackerService.GetRightTeam().Count <= 0;
        if (isLeftPlayerLose || isRightPlayerLose)
        {
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