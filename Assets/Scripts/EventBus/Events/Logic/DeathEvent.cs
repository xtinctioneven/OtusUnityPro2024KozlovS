public struct DeathEvent : IEvent
{
    public HeroEntity HeroEntity;

    public DeathEvent(HeroEntity entity)
    {
        HeroEntity = entity;
    }
}