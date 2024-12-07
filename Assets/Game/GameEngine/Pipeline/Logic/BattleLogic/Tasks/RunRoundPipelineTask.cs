using UnityEngine;
using Zenject;

public sealed class RunRoundPipelineTask : EventTask
{
    private DiContainer _diContainer;
    private EventBus _eventBus;
    private RoundPipeline _roundPipeline;
    private int _roundCount = 10;
    
    public RunRoundPipelineTask(
        DiContainer diContainer,
        EventBus eventBus
    )
    {
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
        _roundCount--;
        _roundPipeline.Reset();
        if (_roundCount <= 0 || Helper.Instance.IsGameOver)
        {
            _roundPipeline.OnFinished -= RoundPipelineOnFinished;
            Finish();
        }
        else
        {
            _roundPipeline.RunNextTask();
        }
    }
}