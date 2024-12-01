public struct AttackEvent : IEvent
{
    public HeroEntity SourceHero;
    public HeroEntity TargetHero;

    public AttackEvent(HeroEntity source, HeroEntity target)
    {
        SourceHero = source;
        TargetHero = target;
    }
}