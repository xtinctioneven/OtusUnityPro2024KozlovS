public class UpdateStatsHandler: BaseHandler<UpdateStatsEvent>
{
    
    public UpdateStatsHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(UpdateStatsEvent evt)
    {
        HeroEntity heroEntity = evt.HeroEntity;
        heroEntity.GetHeroComponent<AttackComponent>().Value = evt.AttackValue;
        heroEntity.GetHeroComponent<LifeComponent>().Value = evt.HPValue;
    }
}