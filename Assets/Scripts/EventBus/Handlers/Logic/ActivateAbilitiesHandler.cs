public class ActivateAbilitiesHandler: BaseHandler<ActivateAbilitiesEvent>
{
    public ActivateAbilitiesHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(ActivateAbilitiesEvent evt)
    {
        if (evt.SourceHero.GetHeroComponent<DeathComponent>().IsDead)
        {
            return;
        }
        IEffect[] heroAbilities = evt.SourceHero.GetHeroComponent<AbilityComponent>().GetAbilities().ToArray();
        for (int i = heroAbilities.Length - 1; i >= 0; i--)
        {
            if (heroAbilities[i].ActivateTurnPhase == evt.TurnPhase)
            {
                heroAbilities[i].SourceHero = evt.SourceHero;
                if (evt.HeroInteractionData != null)
                {
                    heroAbilities[i].HeroInteractionData = evt.HeroInteractionData;
                }
                EventBus.RaiseEventWithPriority(heroAbilities[i]);
            }
        }
    }
}