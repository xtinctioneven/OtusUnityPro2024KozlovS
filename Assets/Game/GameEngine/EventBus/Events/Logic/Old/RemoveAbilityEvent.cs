public struct RemoveAbilityEvent : IEvent
{
    public HeroEntity HeroEntity;
    public IEffectOld Ability;

    public RemoveAbilityEvent(HeroEntity sourceHero, IEffectOld ability)
    {
        HeroEntity = sourceHero;
        Ability = ability;
    }
}