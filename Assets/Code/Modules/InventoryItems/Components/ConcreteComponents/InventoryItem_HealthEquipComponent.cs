using System;

[Serializable]
public class InventoryItem_HealthEquipComponent : IItemComponent, IEquippableComponent
{
    public int HitPoints = 2;
    
    public IItemComponent Clone()
    {
        return new InventoryItem_HealthEquipComponent()
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