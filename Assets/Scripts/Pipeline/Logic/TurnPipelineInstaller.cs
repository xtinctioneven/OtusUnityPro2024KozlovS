using UI;
using Zenject;

public class TurnPipelineInstaller: IInitializable
{
    private readonly DiContainer _diContainer;
    private readonly TurnPipeline _turnPipeline;
    private readonly EventBus _eventBus;
    private readonly UIService _uiService;
    private readonly TurnOrderService _turnOrderService;
    private readonly HeroInteractionService _heroInteractionService;
    private readonly HeroTrackerService _heroTrackerService;
    private readonly VisualPipeline _visualPipeline;
    
    [Inject]
    public TurnPipelineInstaller(
        DiContainer diContainer,
        TurnPipeline turnPipeline,
        EventBus eventBus,
        TurnOrderService turnOrderService,
        UIService uiService, 
        HeroInteractionService heroInteractionService,
        HeroTrackerService heroTrackerService,
        VisualPipeline visualPipeline)
    {
        _diContainer = diContainer;
        _turnPipeline = turnPipeline;
        _eventBus = eventBus;
        _turnOrderService = turnOrderService;
        _uiService = uiService;
        _heroInteractionService = heroInteractionService;
        _heroTrackerService = heroTrackerService;
        _visualPipeline = visualPipeline;
    }

    public void Initialize()
    {
        _turnPipeline.AddTask(new StartTask());
        _turnPipeline.AddTask(new PlayerTurnStartTask(_turnOrderService, _eventBus));
        _turnPipeline.AddTask(new PlayerInputTask(_turnOrderService, _uiService, _eventBus));
        _turnPipeline.AddTask(new HeroesClashTask(_heroInteractionService, _eventBus));
        _turnPipeline.AddTask(new PlayerTurnEndTask( _eventBus, _turnOrderService, _heroTrackerService));
        _turnPipeline.AddTask(new StartVisualPipelineTask(_visualPipeline));
        _turnPipeline.AddTask(new GameOverCheckTask(_eventBus, _heroTrackerService, _turnPipeline));
        _turnPipeline.AddTask(new FinishTask(_heroInteractionService));
    }
}