public struct RemoveAbilityEvent : IEvent
{
    public HeroEntity HeroEntity;
    public IEffect Ability;

    public RemoveAbilityEvent(HeroEntity sourceHero, IEffect ability)
    {
        HeroEntity = sourceHero;
        Ability = ability;
    }
}