using UnityEngine;

public class DestoryVfxTask : EventTask
{
    private ParticleSystem _vfx;

    public DestoryVfxTask(ParticleSystem vfx)
    {
        _vfx = vfx;
    }

    protected override void OnRun()
    {
        _vfx.Stop();
        Object.Destroy(_vfx);
        Finish();
    }
}