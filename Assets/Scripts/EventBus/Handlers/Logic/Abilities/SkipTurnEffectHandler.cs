using UnityEngine;

public class SkipTurnEffectHandler: BaseHandler<SkipTurnEffect>
{
    private readonly TurnPipeline _turnPipeline;
    private readonly TurnOrderService _turnOrderService;
    
    public SkipTurnEffectHandler(EventBus eventBus, TurnPipeline turnPipeline, TurnOrderService turnOrderService) : base(eventBus)
    {
        _turnPipeline = turnPipeline;
        _turnOrderService = turnOrderService;
    }

    protected override void OnHandleEvent(SkipTurnEffect evt)
    {
        if (evt.SourceHero != _turnOrderService.GetActiveHero() ||  evt.SourceHero == null)
        {
            return;
        }
        _turnPipeline.SkipHeroTurn();
        EventBus.RaiseEvent(new RemoveAbilityEvent(evt.SourceHero, evt));
    }
}