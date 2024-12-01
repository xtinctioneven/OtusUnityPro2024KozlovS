using Zenject;

public class RoundPipelineInstaller: IInitializable
{
    private readonly DiContainer _diContainer;
    private readonly RoundPipeline _roundPipeline;
    private readonly EventBus _eventBus;
    
    [Inject]
    public RoundPipelineInstaller(
        DiContainer diContainer,
        RoundPipeline roundPipeline,
        EventBus eventBus
    )
    {
        _diContainer = diContainer;
        _roundPipeline = roundPipeline;
        _eventBus = eventBus;
    }

    public void Initialize()
    { 
        _roundPipeline.AddTask(new StartTask());
        // _roundPipeline.AddTask(new RefreshServicesTask());
        _roundPipeline.AddTask(new StartRoundTask(_diContainer, _eventBus));
        _roundPipeline.AddTask(new RunTurnPipelineTask(_diContainer, _eventBus));
        //_roundPipeline.AddTask(new FinishRoundTask());
        
        
        // _roundPipeline.AddTask(new StartTask());
        // _roundPipeline.AddTask(new PlayerTurnStartTask(_turnOrderService, _eventBus));
        // _roundPipeline.AddTask(new PlayerInputTask(_turnOrderService, _uiService, _eventBus));
        // _roundPipeline.AddTask(new HeroesClashTask(_heroInteractionService, _eventBus));
        //_roundPipeline.AddTask(new PlayerTurnEndTask( _eventBus, _turnOrderService, _heroTrackerService));
        // _roundPipeline.AddTask(new StartVisualPipelineTask(_visualPipeline));
        // _roundPipeline.AddTask(new GameOverCheckTask(_eventBus, _heroTrackerService, _turnPipeline));
        // _roundPipeline.AddTask(new FinishTask(_heroInteractionService));
        //_roundPipeline.AddTask(new FinishTurnTask());
        //_roundPipeline.AddTask(new FinishRoundTask());
        _roundPipeline.AddTask(new FinishTask());
    }
}