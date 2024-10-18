using System;

[Serializable]
public class InventoryItem_ManaConsumeComponent : IItemComponent, IConsumableComponent
{
    public int ManaValue;
    public IItemComponent Clone()
    {
        return new InventoryItem_ManaConsumeComponent()
        {
            ManaValue = ManaValue,
        };
    }

    public void Apply(IHero hero)
    {
        hero.Mana += ManaValue;
    }
}