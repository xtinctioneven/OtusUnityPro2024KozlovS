using UnityEngine;

public class SkipTurnEffectHandler: BaseHandler<SkipTurnEffectOld>
{
    private readonly TurnPipeline _turnPipeline;
    private readonly TurnOrderServiceOld _turnOrderService;
    
    public SkipTurnEffectHandler(EventBus eventBus, TurnPipeline turnPipeline, TurnOrderServiceOld turnOrderService) : base(eventBus)
    {
        _turnPipeline = turnPipeline;
        _turnOrderService = turnOrderService;
    }

    protected override void OnHandleEvent(SkipTurnEffectOld evt)
    {
        if (evt.SourceHero != _turnOrderService.GetActiveHero() ||  evt.SourceHero == null)
        {
            return;
        }
        _turnPipeline.SkipHeroTurn();
        EventBus.RaiseEvent(new RemoveAbilityEvent(evt.SourceHero, evt));
    }
}