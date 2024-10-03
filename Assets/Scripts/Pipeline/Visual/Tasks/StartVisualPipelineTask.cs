using UnityEngine;

public class StartVisualPipelineTask : EventTask
{
    private readonly VisualPipeline _visualPipeline;

    public StartVisualPipelineTask(VisualPipeline visualPipeline)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void OnRun()
    {
        Debug.Log("Run visual pipeline");
        _visualPipeline.OnFinished += VisualPipelineOnFinished;
        _visualPipeline.Reset();
        _visualPipeline.RunNextTask();
    }

    private void VisualPipelineOnFinished()
    {
        _visualPipeline.OnFinished -= VisualPipelineOnFinished;
        _visualPipeline.ClearAll();
        Finish();
    }
}