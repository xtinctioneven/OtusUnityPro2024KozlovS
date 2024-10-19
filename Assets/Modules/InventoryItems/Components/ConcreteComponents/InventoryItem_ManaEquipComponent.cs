using System;

[Serializable]
public class InventoryItem_ManaEquipComponent : IItemComponent, IEquippableComponent
{
    public int Mana = 2;
    
    public IItemComponent Clone()
    {
        return new InventoryItem_ManaEquipComponent()
        {
            Mana = Mana,
        };
    }
    
    public void Apply(IHero hero)
    {
        hero.Mana += Mana;
    }

    public void Discard(IHero hero)
    {
        hero.Mana -= Mana;
    }
}