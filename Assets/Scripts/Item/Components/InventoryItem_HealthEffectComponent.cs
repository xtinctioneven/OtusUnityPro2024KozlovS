using System;

[Serializable]
public class InventoryItem_HealthEffectComponent : IItemComponent, IEffectableComponent
{
    public int HitPoints = 2;
    
    public IItemComponent Clone()
    {
        return new InventoryItem_HealthEffectComponent()
        {
            HitPoints = HitPoints,
        };
    }
    
    public void Apply(Hero hero)
    {
        hero.MaxHitPoints += HitPoints;
    }

    public void Discard(Hero hero)
    {
        hero.MaxHitPoints -= HitPoints;
    }
}