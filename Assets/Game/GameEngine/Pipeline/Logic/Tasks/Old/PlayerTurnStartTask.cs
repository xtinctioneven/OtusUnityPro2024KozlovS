public class PlayerTurnStartTask : EventTask
{
    private HeroEntity _activeHeroEntity;
    private readonly EventBus _eventBus;
    private readonly TurnOrderService _turnOrderService;
    
    public PlayerTurnStartTask(
        TurnOrderService turnOrderService,
        EventBus eventBus)
    {
        _turnOrderService = turnOrderService;
        _eventBus = eventBus;
    }

    protected override void OnRun()
    {
        // _activeHeroEntity = _turnOrderService.ActivateNextHero();
        _eventBus.RaiseEvent(new ActivateHeroEvent(_activeHeroEntity));
        _eventBus.RaiseEvent(new ActivateAbilitiesEvent(TurnPhase.PlayerTurnStart, _activeHeroEntity));
        Finish();
    }
}