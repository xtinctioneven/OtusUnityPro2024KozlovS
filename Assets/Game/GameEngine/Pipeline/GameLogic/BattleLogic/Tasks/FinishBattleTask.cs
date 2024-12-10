using UnityEngine;

public sealed class FinishBattleTask : EventTask
{
    //private HeroInteractionService _heroInteractionService;

    public FinishBattleTask(
        //HeroInteractionService heroInteractionService
    )
    {
        //_heroInteractionService = heroInteractionService;
    }

    protected override void OnRun()
    {
        Debug.Log("Run finish battle task");
        //_heroInteractionService.Reset();
        Finish();
    }
}