using Game.Gameplay;
using Zenject;

public class BattlePipelineInstaller: IInitializable
{
    private readonly DiContainer _diContainer;
    private readonly BattlePipeline _battlePipeline;
    private readonly EventBus _eventBus;
    private TargetFinderService _targetFinderService;
    private EntityTrackerService _entityTrackerService;
    private LinkEffectsTracker _linkEffectsTracker;
    
    [Inject]
    public BattlePipelineInstaller(
        DiContainer diContainer,
        EventBus eventBus,
        BattlePipeline battlePipeline,
        TargetFinderService targetFinderService,
        LinkEffectsTracker linkEffectsTracker,
        EntityTrackerService entityTrackerService
        )
    {
        _battlePipeline = battlePipeline;
        _targetFinderService = targetFinderService;
        _linkEffectsTracker = linkEffectsTracker;
        _entityTrackerService = entityTrackerService;
        _diContainer = diContainer;
        _eventBus = eventBus;
    }

    public void Initialize()
    { 
        _battlePipeline.AddTask(new SetupServicesTask(_targetFinderService, _linkEffectsTracker, _entityTrackerService)); //Определить порядок хода, инициализировать сервисы типа HeroTracker
        _battlePipeline.AddTask(new SetupAbilityControllersTask(_diContainer, _eventBus)); //=> SetupObservers??? Пусть будут сервисы-обсерверы на каждый триггер
        _battlePipeline.AddTask(new StartBattleTask());
        _battlePipeline.AddTask(new RunRoundPipelineTask(_diContainer, _eventBus));
        _battlePipeline.AddTask(new FinishBattleTask());
    }
}