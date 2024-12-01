using Game.Gameplay;
using UnityEngine;
using Zenject;

public class EvasionPassiveEffectHandler : BaseHandler<EvasionPassiveEffect>
{
    private DiContainer _diContainer;
    
    public EvasionPassiveEffectHandler(EventBus eventBus,
        DiContainer diDiContainer
    ) : base(eventBus)
    {
        _diContainer = diDiContainer;
    }

    protected override void OnHandleEvent(EvasionPassiveEffect evt)
    { 
        EntityInteractionData interactionData = evt.InteractionData;
        if (!evt.InteractionTypesToDodge.Contains(interactionData.SourceEffect.InteractionType) || 
            !(evt as IEffectPassive).CanBeUsed ||
            evt.SourceEntity == interactionData.SourceEntity)
        {
            return;
        }

        float value = Random.value;
        Debug.Log(value);
        if (evt.EvasionProbability >= value)
        {
            interactionData.InteractionResult = InteractionResult.Dodge;
            evt.TryUseCount();
        }
    }
}