using Game.Gameplay;
using UnityEngine;
using Zenject;

public class AddCountToSourceOnResultPassiveEffectHandler : BaseHandler<AddCountToSourceOnResultPassiveEffect>
{
    private DiContainer _diContainer;
    
    public AddCountToSourceOnResultPassiveEffectHandler(EventBus eventBus,
        DiContainer diDiContainer
    ) : base(eventBus)
    {
        _diContainer = diDiContainer;
    }

    protected override void OnHandleEvent(AddCountToSourceOnResultPassiveEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        if (evt.SourceEntity != interactionData.SourceEntity 
            || !(evt as IEffectPassive).CanBeUsed 
            || !evt.InteractionTypesToTrigger.Contains(interactionData.SourceEffect.InteractionType)  
            || !evt.InteractionResultsToTrigger.Contains(interactionData.InteractionResult)
           )
        {
            return;
        }

        if (interactionData.SourceEffect is IEffectUseCounts sourceEffect && evt.EffectProbability >= Random.value)
        {
            Debug.Log("Adding counts to source effect.");
            sourceEffect.AddCount(evt.AddCountsOnTrigger);
        }
    }
}