using Game.Gameplay;

public struct RemoveStatusEffectEvent: IEvent
{
    public IStatusEffect StatusEffect;
    
    public RemoveStatusEffectEvent(IStatusEffect statusEffect)
    {
        StatusEffect = statusEffect;
    }
}