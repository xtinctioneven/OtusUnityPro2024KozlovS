public class HeroInteractionService
{
    private EventBus _eventBus;
    private HeroEntity _activeHero;
    private HeroEntity _targetHero;
    private HeroInteractionData _currentInteractionData;
    
    public HeroInteractionService()
    {
    }

    public void SetActiveHero(HeroEntity activeHero)
    {
        _activeHero = activeHero;
    }
    
    public void SetTargetHero(HeroEntity targetHero)
    {
        _targetHero = targetHero;
    }

    public void Reset()
    {
        _activeHero = null;
        _targetHero = null;
        _currentInteractionData = null;
    }

    public HeroInteractionData CreateCurrentClashData(HeroEntity sourceHero = null, HeroEntity targetHero = null)
    {
        HeroEntity tempSourceHero = sourceHero ?? _activeHero;
        HeroEntity tempTargetHero = targetHero ?? _targetHero;
        _currentInteractionData = new HeroInteractionData
        {
            SourceHero = tempSourceHero,
            TargetHero = tempTargetHero,
            TargetHeroDamageReceived = 0,
            SourceHeroDamageReceived = 0,
            SourceHeroHealth = tempSourceHero.GetHeroComponent<LifeComponent>().Value,
            TargetHeroHealth = tempTargetHero.GetHeroComponent<LifeComponent>().Value
        };
        return _currentInteractionData;
    }

    public HeroInteractionData GetCurrentClashData()
    {
        return _currentInteractionData;
    }

    public HeroInteractionData CreateEmptyInteractionData(HeroEntity sourceHero = null, HeroEntity targetHero = null)
    {
        HeroEntity tempSourceHero = sourceHero ?? _activeHero;
        HeroEntity tempTargetHero = targetHero ?? _targetHero;
        HeroInteractionData tempData = new HeroInteractionData
        {
            SourceHero = tempSourceHero,
            TargetHero = tempTargetHero,
            TargetHeroDamageReceived = 0,
            SourceHeroDamageReceived = 0,
            SourceHeroHealth = tempSourceHero.GetHeroComponent<LifeComponent>().Value,
            TargetHeroHealth = tempTargetHero.GetHeroComponent<LifeComponent>().Value
        };
        return tempData;
    }
}

public class HeroInteractionData
{
    public HeroEntity SourceHero;
    public HeroEntity TargetHero;
    public int SourceHeroDamageReceived;
    public int TargetHeroDamageReceived;
    public int SourceHeroHealth;
    public int TargetHeroHealth;
}