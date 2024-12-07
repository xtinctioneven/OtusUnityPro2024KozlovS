using Zenject;

public class CharacterTurnStartTask : EventTask
{
    private DiContainer _diContainer;
    
    public CharacterTurnStartTask(
        DiContainer diContainer,
        EventBus eventBus)
    {
        _diContainer = diContainer;
    }

    protected override void OnRun()
    {
        var activeEntity = _diContainer.Resolve<TurnOrderService>().ActivateNextEntity();
        _diContainer.Resolve<EntityInteractionService>().SetActiveEntity(activeEntity);
        Finish();
    }
}