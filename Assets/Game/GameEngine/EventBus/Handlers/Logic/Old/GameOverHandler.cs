using UI;
using UnityEngine;

public class GameOverHandler: BaseHandler<GameOverEvent>
{
    private UIService _uiService;
    private readonly TurnPipeline _turnPipeline;
    private readonly TurnOrderService _turnOrderService;
    
    public GameOverHandler(EventBus eventBus, TurnPipeline turnPipeline, TurnOrderService turnOrderService) : base(eventBus)
    {
        _turnPipeline = turnPipeline;
        _turnOrderService = turnOrderService;
    }

    protected override void OnHandleEvent(GameOverEvent evt)
    {
        _turnOrderService.Clear();
        _turnPipeline.AddTask(new IdleTask());
    }
}