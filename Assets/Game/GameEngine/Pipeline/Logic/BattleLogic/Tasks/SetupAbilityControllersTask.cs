using Unity.VisualScripting;
using Zenject;

public class SetupAbilityControllersTask : EventTask
{
    private readonly EventBus _eventBus;
    private readonly DiContainer _diContainer;
    private AbilityService _abilityService;
    //private readonly List<IAbilityController> _abilityControllersList;
    // private EntityTrackerService _entityTrackerService;
    // private TurnOrderService _turnOrderService;
    // private EntityInteractionService _entityInteractionService;
    
    public SetupAbilityControllersTask(
        DiContainer diContainer,
        EventBus eventBus)
    {
        _diContainer = diContainer;
        _eventBus = eventBus;
    }

    protected override void OnRun()
    {
        //TODO: Delete?
        //BattlefieldModel battlefieldModel = _diContainer.Resolve<BattlefieldModel>();
        // _entityTrackerService = new EntityTrackerService(_eventBus, battlefieldModel);
        // _entityTrackerService.Initialize();
        // _diContainer.Bind<EntityTrackerService>().FromInstance(_entityTrackerService).AsSingle().NonLazy();
        //
        // _turnOrderService = new TurnOrderService(_eventBus, _entityTrackerService);
        // _entityTrackerService.Initialize();
        // _diContainer.Bind<TurnOrderService>().FromInstance(_turnOrderService).AsSingle().NonLazy();
        //
        // _entityInteractionService = new EntityInteractionService();
        // _diContainer.Bind<EntityInteractionService>().FromInstance(_entityInteractionService).AsSingle().NonLazy();
        
        Finish();
    }
}

public interface IAbilityController
{
    
}