using Game.Gameplay;
using Zenject;

public class DamageMaxHpPercentStatusEffectHandler : BaseHandler<DamageMaxHpPercentStatusEffect>
{
    public DamageMaxHpPercentStatusEffectHandler(EventBus eventBus,
        DiContainer diContainer
    ) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(DamageMaxHpPercentStatusEffect evt)
    { 
        // EntityInteractionData interactionData = evt.InteractionData;
        //
        // if (evt.AfflictedEntity != interactionData.SourceEntity 
        //     || !(interactionData.SourceEffect is IEffectUseCounts sourceEffect) 
        //    )
        // {
        //     return;
        // }
        // sourceEffect.AddCount(evt.AddCounts);
    }
}