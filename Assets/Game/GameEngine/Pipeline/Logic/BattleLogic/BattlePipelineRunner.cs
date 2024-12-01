using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class BattlePipelineRunner : MonoBehaviour
{
    private BattlePipeline _battlePipeline;

    [Inject]
    public void Construct(BattlePipeline battlePipeline)
    {
        _battlePipeline = battlePipeline;
    }

    private void Start()
    {
        _battlePipeline.OnFinished += OnFinished;
        //Run();
    }

    private void OnFinished()
    {
        Debug.Log("Battle Finished!");
        // _battlePipeline.Reset();
        // _battlePipeline.RunNextTask();
    }

    [Button]
    public void Run()
    {
        _battlePipeline.RunNextTask();
    }
}