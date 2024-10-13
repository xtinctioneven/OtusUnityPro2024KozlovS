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
    
    public void Apply(Hero hero)
    {
        hero.Mana += Mana;
    }

    public void Discard(Hero hero)
    {
        hero.Mana -= Mana;
    }
}