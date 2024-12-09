using Zenject;

public class CharacterTurnStartTask : EventTask
{
    private readonly TurnOrderService _turnOrderService;
    private readonly EntityInteractionService _entityInteractionService;
    
    public CharacterTurnStartTask(
        TurnOrderService turnOrderService,
        EntityInteractionService entityInteractionService)
    {
        _turnOrderService = turnOrderService;
        _entityInteractionService = entityInteractionService;
    }

    protected override void OnRun()
    {
        var activeEntity = _turnOrderService.ActivateNextEntity();
        _entityInteractionService.SetActiveEntity(activeEntity);
        Finish();
    }
}