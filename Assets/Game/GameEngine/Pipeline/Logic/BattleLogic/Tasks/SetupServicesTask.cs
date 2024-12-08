using Game.Gameplay;
using Zenject;

public class SetupServicesTask : EventTask
{
    //TODO: Перенести в некий лоадинг пайплайн, если дойдет до разделения сцен
    private EventBus _eventBus;
    private DiContainer _diContainer;
    private TargetFinderService _targetFinderService;
    private EntityTrackerService _entityTrackerService;
    private LinkEffectsTracker _linkEffectsTracker;

    [Inject]
    public void Construct(
        TargetFinderService targetFinderService,
        LinkEffectsTracker linkEffectsTracker,
        EntityTrackerService entityTrackerService,
        DiContainer diContainer,
        EventBus eventBus)
    {
        _targetFinderService = targetFinderService;
        _linkEffectsTracker = linkEffectsTracker;
        _entityTrackerService = entityTrackerService;
        _diContainer = diContainer;
        _eventBus = eventBus;
    }
    
    public SetupServicesTask(
        TargetFinderService targetFinderService,
        LinkEffectsTracker linkEffectsTracker,
        EntityTrackerService entityTrackerService
        )
    {
        _targetFinderService = targetFinderService;
        _linkEffectsTracker = linkEffectsTracker;
        _entityTrackerService = entityTrackerService;
    }

    protected override void OnRun()
    {
        //BattlefieldModel battlefieldModel = _diContainer.Resolve<BattlefieldModel>();
        
        // _entityTrackerService = new EntityTrackerService(_eventBus, _diContainer);
       // _diContainer.Bind<EntityTrackerService>().FromInstance(_entityTrackerService).AsSingle().NonLazy();
        
        // _turnOrderService = new TurnOrderService(_entityTrackerService);
        //_diContainer.Bind<TurnOrderService>().FromInstance(_turnOrderService).AsSingle().NonLazy();

        // _entityInteractionService = new EntityInteractionService();
        // _diContainer.Bind<EntityInteractionService>().FromInstance(_entityInteractionService).AsSingle().NonLazy();

        // _targetFinderService = new TargetFinderService(_diContainer);
        // _diContainer.Bind<TargetFinderService>().FromInstance(_targetFinderService).AsSingle().NonLazy();

        // _triggerEffectsTracker = new TriggerEffectsTracker(_eventBus, _diContainer);
        // _diContainer.Bind<TriggerEffectsTracker>().FromInstance(_triggerEffectsTracker).AsSingle().NonLazy();
        
        // _linkEffectsTracker = new LinkEffectsTracker(_diContainer);
        // _diContainer.Bind<LinkEffectsTracker>().FromInstance(_linkEffectsTracker).AsSingle().NonLazy();
        //
        // _abilityService = new AbilityService(_diContainer, _eventBus);
        // _diContainer.Bind<AbilityService>().FromInstance(_abilityService).AsSingle().NonLazy();
        
        _targetFinderService.Initialize();
        _linkEffectsTracker.Initialize();
        _entityTrackerService.Initialize();
        Finish();
    }
}