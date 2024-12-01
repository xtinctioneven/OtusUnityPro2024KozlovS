using UnityEngine;
using Zenject;

public sealed class RunRoundPipelineTask : EventTask
{
    //private HeroInteractionService _heroInteractionService;
    private DiContainer _diContainer;
    private EventBus _eventBus;
    private RoundPipeline _roundPipeline;
    private int _roundCount = 10;
    
    public RunRoundPipelineTask(
        DiContainer diContainer,
        EventBus eventBus
    )
    {
        //_heroInteractionService = heroInteractionService;
        _diContainer = diContainer;
        _eventBus = eventBus;
    }

    protected override void OnRun()
    {
        _roundPipeline = _diContainer.Resolve<RoundPipeline>();
        _roundPipeline.OnFinished += RoundPipelineOnFinished;
        _roundPipeline.RunNextTask();
    }

    private void RoundPipelineOnFinished()
    {
        // if (_turnOrderService.IsQueueEmpty)
        // {
        //     _roundCount--;
        // }
        //
        // if (_roundCount == 0 || Helper.Instance.IsGameOver)
        // {
        //     _turnPipeline.OnFinished -= TurnPipelineOnFinished;
        //     Finish();
        // }
        // else
        // {
        //     _turnPipeline.Reset();
        //     _turnPipeline.RunNextTask();
        // }
        _roundCount--;
        _roundPipeline.Reset();
        if (_roundCount <= 0 || Helper.Instance.IsGameOver)
        {
            _roundPipeline.OnFinished -= RoundPipelineOnFinished;
            // Debug.Log("Run Round Pipeline task finished");
            //_heroInteractionService.Reset();
            Finish();
        }
        else
        {
            _roundPipeline.RunNextTask();
        }
    }
}