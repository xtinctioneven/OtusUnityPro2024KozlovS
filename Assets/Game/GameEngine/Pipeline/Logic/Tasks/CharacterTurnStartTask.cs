using Zenject;

public class CharacterTurnStartTask : EventTask
{
    // private HeroEntity _activeHeroEntity;
    // private TurnOrderService _turnOrderService;
    private readonly EventBus _eventBus;
    private DiContainer _diContainer;
    
    public CharacterTurnStartTask(
        // TurnOrderService turnOrderService,
        DiContainer diContainer,
        EventBus eventBus)
    {
        // _turnOrderService = turnOrderService;
        _diContainer = diContainer;
        _eventBus = eventBus;
    }

    protected override void OnRun()
    {
        //var turnOrderService = _diContainer.Resolve<TurnOrderService>();
        var activeEntity = _diContainer.Resolve<TurnOrderService>().ActivateNextEntity();
        _diContainer.Resolve<EntityInteractionService>().SetActiveEntity(activeEntity);
        // _eventBus.RaiseEvent(new ActivateHeroEvent(_activeHeroEntity));
        // _eventBus.RaiseEvent(new ActivateAbilitiesEvent(TurnPhase.PlayerTurnStart, _activeHeroEntity));
        Finish();
    }
}