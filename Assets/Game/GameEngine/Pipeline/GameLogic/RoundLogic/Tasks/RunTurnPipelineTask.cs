using UnityEngine;
using Zenject;

public sealed class RunTurnPipelineTask : EventTask
{
    private DiContainer _diContainer;
    private EventBus _eventBus;
    private TurnPipeline _turnPipeline;
    private TurnOrderService _turnOrderService;
    
    public RunTurnPipelineTask(
        DiContainer diContainer,
        EventBus eventBus
    )
    {
        _diContainer = diContainer;
        _eventBus = eventBus;
    }

    protected override void OnRun()
    {
        _turnOrderService = _diContainer.Resolve<TurnOrderService>();
        _turnPipeline = _diContainer.Resolve<TurnPipeline>();
        _turnPipeline.OnFinished += TurnPipelineOnFinished;
        _turnPipeline.RunNextTask();
    }

    private void TurnPipelineOnFinished()
    {
        _turnPipeline.Reset();
        if (_turnOrderService.IsQueueEmpty || Helper.Instance.IsGameOver)
        {
            _turnPipeline.OnFinished -= TurnPipelineOnFinished;
            Finish();
        }
        else
        {
            _turnPipeline.RunNextTask();
        }
    }
}