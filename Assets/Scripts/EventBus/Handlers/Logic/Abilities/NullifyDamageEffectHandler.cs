public class NullifyDamageEffectHandler: BaseHandler<NullifyDamageEffect>
{
    public NullifyDamageEffectHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(NullifyDamageEffect evt)
    {
        if (evt.SourceHero == null)
        {
            return;
        }
        HeroInteractionData effectInteractionData = evt.HeroInteractionData;
        if (evt.SourceHero == effectInteractionData.SourceHero)
        {
            if (effectInteractionData.SourceHeroDamageReceived > 0)
            {
                effectInteractionData.SourceHeroDamageReceived = 0;
                EventBus.RaiseEvent(new RemoveAbilityEvent(effectInteractionData.SourceHero, evt));
            }
        }

        if (evt.SourceHero == effectInteractionData.TargetHero)
        {
            if (effectInteractionData.TargetHeroDamageReceived > 0)
            {
                effectInteractionData.TargetHeroDamageReceived = 0;
                EventBus.RaiseEvent(new RemoveAbilityEvent(effectInteractionData.TargetHero, evt));
            }
        }
    }
}