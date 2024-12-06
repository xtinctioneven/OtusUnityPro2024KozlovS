using Game.Gameplay;
using UnityEngine;
using Zenject;

public class AddCountToSourceStatusEffectHandler : BaseHandler<AddCountToSourceStatusEffect>
{
    public AddCountToSourceStatusEffectHandler(EventBus eventBus,
        DiContainer diContainer
    ) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(AddCountToSourceStatusEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        
        if (evt.AfflictedEntity != interactionData.SourceEntity 
            || !(interactionData.SourceEffect is IEffectUseCounts sourceEffect) 
           )
        {
            return;
        }
        sourceEffect.AddCount(evt.AddCounts);
    }
}