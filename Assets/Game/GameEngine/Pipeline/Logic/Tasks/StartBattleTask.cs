using UnityEngine;

public sealed class StartBattleTask : EventTask
{
    //TODO: Call OnBattleStart and raise appropriate effects
    protected override void OnRun()
    {
        Debug.Log("<color=red>Run start battle task</color>");
        Finish();
    }
}