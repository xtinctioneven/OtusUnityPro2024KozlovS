public struct UpdateStatsEvent : IEvent
{
    public HeroEntity HeroEntity;
    public int HPValue;
    public int AttackValue;

    public UpdateStatsEvent(HeroEntity hero, int attackValue, int hpValue)
    {
        HeroEntity = hero;
        HPValue = hpValue;
        AttackValue = attackValue;
    }
}