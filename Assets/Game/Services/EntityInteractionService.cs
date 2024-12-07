using System.Collections.Generic;
using Game.Gameplay;

public class EntityInteractionService
{
    private EventBus _eventBus;
    private IEntity _activeEntity;
    private IEntity _mainTargetEntity;
    private EntityInteractionData _currentInteractionData;
    
    public EntityInteractionService()
    {
    }

    public void SetActiveEntity(IEntity activeEntity)
    {
        _activeEntity = activeEntity;
    }
    
    public void SetTargetEntity(IEntity targetEntity)
    {
        _mainTargetEntity = targetEntity;
    }

    public void Reset()
    {
        _activeEntity = null;
        _mainTargetEntity = null;
        _currentInteractionData = null;
    }

    // public EntityInteractionData CreateCurrentInteractionData(IEntity sourceEntity = null, IEntity targetEntity = null)
    // {
    //     IEntity tempSourceEntity = sourceEntity ?? _activeEntity;
    //     IEntity tempTargetEntity = targetEntity ?? _mainTargetEntity;
    //     _currentInteractionData = new EntityInteractionData
    //     {
    //         SourceEntity = tempSourceEntity,
    //         TargetEntity = tempTargetEntity,
    //     };
    //     return _currentInteractionData;
    // }

    public EntityInteractionData GetCurrentInteractionData()
    {
        return _currentInteractionData;
    }

    public EntityInteractionData CreateInteractionData(IEntity sourceEntity = null, IEntity targetEntity = null)
    {
        IEntity tempSourceHero = sourceEntity ?? _activeEntity;
        IEntity tempTargetHero = targetEntity ?? _mainTargetEntity;
        _currentInteractionData = new EntityInteractionData
        {
            SourceEntity = tempSourceHero,
            TargetEntity = tempTargetHero,
        };
        return _currentInteractionData;
    }
}

public class EntityInteractionData
{
    public IEntity SourceEntity;
    public IEntity TargetEntity;
    public IEffect SourceEffect;
    public int SourceEntityDamageOutgoing = 0;
    public int TargetEntityDamageOutgoing = 0;
    public int SourceEntityDamageReceived = 0;
    public int TargetEntityDamageReceived = 0;
    public int SourceEntityHealOutgoing = 0;
    public int TargetEntityHealOutgoing = 0;
    public int SourceEntityHealReceived = 0;
    public int TargetEntityHealReceived = 0;
    public float CriticalDamageMultiplier = 1.5f;
    public List<IStatusEffect> SourceInitialStatusEffects = new();
    public List<IStatusEffect> TargetInitialStatusEffects = new();
    public List<IStatusEffect> StatusEffectsApplyToSource = new();
    public List<IStatusEffect> StatusEffectsApplyToTarget = new();
    public InteractionResult InteractionResult;
}

public enum InteractionResult
{
    Default = 0,
    Hit = 10,
    Dodge = 20,
    CriticalHit = 30,
    Block = 40,
    Heal = 50,
    Buff = 60,
    Passive = 70,
    Kill = 80,
    StatusEffectTick = 90
}

public enum InteractionType
{
    Default = 0,
    StandardStrike = 10,
    Heal = 20,
    Buff = 30,
    Passive = 40,
    StatusEffectTick = 50,
    LinkStrike = 60
}