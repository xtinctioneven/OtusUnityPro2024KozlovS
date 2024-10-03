using System;

public class HeroStatsResolveHandler: BaseHandler<HeroStatsResolveEvent>
{
    
    public HeroStatsResolveHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(HeroStatsResolveEvent evt)
    {
        HeroEntity heroEntity = evt.HeroEntity;
        HeroInteractionData interactionData = evt.HeroInteractionData;
        LifeComponent lifeComponent = heroEntity.GetHeroComponent<LifeComponent>();
        int heroHpResult;
        if (interactionData.TargetHero == heroEntity)
        {
            heroHpResult = lifeComponent.Value - interactionData.TargetHeroDamageReceived;
        }
        else if (interactionData.SourceHero == heroEntity)
        {
            heroHpResult = lifeComponent.Value - interactionData.SourceHeroDamageReceived;
        }
        else
        {
            throw new Exception("Target Hero doesn't exist!");
        }
        int heroAttackResult = heroEntity.GetHeroComponent<AttackComponent>().Value;
        heroHpResult = Math.Clamp(heroHpResult, LifeComponent.MIN_LIFE, lifeComponent.MaxValue);
        EventBus.RaiseEvent(new UpdateStatsEvent(heroEntity, heroAttackResult, heroHpResult));
        if (heroHpResult <= 0)
        {
            EventBus.RaiseEvent(new DeathEvent(heroEntity));
        }
    }
}