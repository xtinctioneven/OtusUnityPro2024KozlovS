public struct ActivateHeroEvent: IEvent
{
    public readonly HeroEntity HeroEntity;
    public ActivateHeroEvent(HeroEntity activatedHeroEntity)
    {
        HeroEntity = activatedHeroEntity;
    }
}