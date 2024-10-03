using UI;

public class DeactivateHeroVisualTask : EventTask
{
    private readonly HeroView _heroView;

    public DeactivateHeroVisualTask(HeroView heroView)
    {
        _heroView = heroView;
    }

    protected override void OnRun()
    {
        if (!_heroView || !_heroView.isActiveAndEnabled || _heroView == null || !_heroView.enabled)
        {
            Finish();
        }
        _heroView.SetActive(false);
        Finish();
    }
}