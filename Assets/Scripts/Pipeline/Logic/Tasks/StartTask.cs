using UnityEngine;

public sealed class StartTask : EventTask
{
    protected override void OnRun()
    {
        Debug.Log("<color=red>Run start task</color>");
        Finish();
    }
}