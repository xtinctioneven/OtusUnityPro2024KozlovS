using UI;

public struct TargetHeroClickedEvent: IEvent
{
    public HeroView HeroView;
    public TargetHeroClickedEvent(HeroView clickedHeroView)
    {
        HeroView = clickedHeroView;
    }
}