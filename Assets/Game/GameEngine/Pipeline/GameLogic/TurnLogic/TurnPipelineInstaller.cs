using Zenject;

public class TurnPipelineInstaller : IInitializable
{
    private readonly TurnPipeline _turnPipeline;
    private readonly EventBus _eventBus;
    private readonly TurnOrderService _turnOrderService;
    private readonly EntityInteractionService _entityInteractionService;
    private readonly EntityTrackerService _entityTrackerService;
    private readonly AbilityService _abilityService;
    private readonly VisualPipeline _visualPipeline;
    
    [Inject]
    public TurnPipelineInstaller(
        TurnPipeline turnPipeline,
        EventBus eventBus,
        TurnOrderService turnOrderService,
        EntityInteractionService entityInteractionService,
        AbilityService abilityService,
        EntityTrackerService entityTrackerService,
        VisualPipeline visualPipeline
        )
    {
        _turnPipeline = turnPipeline;
        _eventBus = eventBus;
        _turnOrderService = turnOrderService;
        _entityInteractionService = entityInteractionService;
        _abilityService = abilityService;
        _entityTrackerService = entityTrackerService;
        _visualPipeline = visualPipeline;
    }

    public void Initialize()
    {
        _turnPipeline.AddTask(new CharacterTurnStartTask(_turnOrderService, _entityInteractionService));
        _turnPipeline.AddTask(new EntityActionTask(_abilityService, _turnOrderService, _visualPipeline));
        _turnPipeline.AddTask(new FinishTurnTask(_turnOrderService, _entityInteractionService));
        _turnPipeline.AddTask(new GameOverCheckTask(_eventBus, _entityTrackerService));
        _turnPipeline.AddTask(new StartVisualPipelineTask(_visualPipeline));
    }
}