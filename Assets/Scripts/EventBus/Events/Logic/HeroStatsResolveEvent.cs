public struct HeroStatsResolveEvent: IEvent
{
    public HeroEntity HeroEntity;
    public HeroInteractionData HeroInteractionData;
    public HeroStatsResolveEvent(HeroEntity heroEntity, HeroInteractionData heroInteractionData)
    {
        HeroEntity = heroEntity;
        HeroInteractionData = heroInteractionData;
    }
}