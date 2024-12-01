using UnityEngine;

public sealed class FinishTask : EventTask
{
    //private HeroInteractionService _heroInteractionService;

    public FinishTask(
        //HeroInteractionService heroInteractionService
        )
    {
        //_heroInteractionService = heroInteractionService;
    }

    protected override void OnRun()
    {
        Debug.Log("Run finish task");
        //_heroInteractionService.Reset();
        Finish();
    }
}