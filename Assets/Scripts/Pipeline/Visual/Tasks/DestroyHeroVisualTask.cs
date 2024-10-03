using System.Collections.Generic;
using System.Linq;
using UI;

public class DestroyHeroVisualTask : EventTask
{
    private readonly HeroEntity _heroEntity;

    public DestroyHeroVisualTask(HeroEntity heroEntity)
    {
        _heroEntity = heroEntity;
    }

    protected override void OnRun()
    {
        _heroEntity.GetHeroComponent<DestroyComponent>().Destroy();
        Finish();
    }
}