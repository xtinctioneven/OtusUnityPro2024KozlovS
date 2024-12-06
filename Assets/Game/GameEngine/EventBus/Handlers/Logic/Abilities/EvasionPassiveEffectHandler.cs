using Game.Gameplay;
using UnityEngine;
using Zenject;

public class EvasionPassiveEffectHandler : BaseHandler<EvasionPassiveEffect>
{
    private DiContainer _diContainer;
    
    public EvasionPassiveEffectHandler(EventBus eventBus,
        DiContainer diContainer
    ) : base(eventBus)
    {
        _diContainer = diContainer;
    }

    protected override void OnHandleEvent(EvasionPassiveEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        if (!evt.InteractionTypesToDodge.Contains(interactionData.SourceEffect.InteractionType)  
            || !(evt as IEffectPassive).CanBeUsed 
            || evt.SourceEntity == interactionData.SourceEntity 
            || !evt.InteractionResultsToDodge.Contains(interactionData.InteractionResult)
            )
        {
            return;
        }

        if (evt.EffectProbability >= Random.value)
        {
            interactionData.InteractionResult = InteractionResult.Dodge;
            evt.TryUseCount();
        }
    }
}