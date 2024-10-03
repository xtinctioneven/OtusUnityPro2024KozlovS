public struct HeroInteractionEvent: IEvent
{
    public HeroInteractionData HeroInteractionData;
    public HeroInteractionEvent(HeroInteractionData heroInteractionData)
    {
        HeroInteractionData = heroInteractionData;
    }
}