using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class TurnPipelineRunner : MonoBehaviour
{
    private TurnPipeline _turnPipeline;

    [Inject]
    public void Construct(TurnPipeline turnPipeline)
    {
        _turnPipeline = turnPipeline;
    }

    private void Start()
    {
        _turnPipeline.OnFinished += OnFinished;
        //Run();
    }

    private void OnFinished()
    {
        _turnPipeline.Reset();
        _turnPipeline.RunNextTask();
    }

    [Button]
    public void Run()
    {
        _turnPipeline.RunNextTask();
    }
}