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
    
    public void Apply(IHero hero)
    {
        hero.MaxHitPoints += HitPoints;
    }

    public void Discard(IHero hero)
    {
        hero.MaxHitPoints -= HitPoints;
    }
}