using UnityEngine;

public sealed class IdleTask : EventTask
{
    protected override void OnRun()
    {
        Debug.Log("Switch to idle mode");
    }
}