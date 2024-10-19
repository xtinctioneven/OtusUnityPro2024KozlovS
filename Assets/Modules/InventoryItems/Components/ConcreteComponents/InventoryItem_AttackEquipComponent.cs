using System;

[Serializable]
public class InventoryItem_AttackEquipComponent : IItemComponent, IEquippableComponent
{
     public int Attack = 5;
     
     public IItemComponent Clone()
     {
         return new InventoryItem_AttackEquipComponent()
         {
             Attack = Attack,
         };
     }
     
     public void Apply(IHero hero)
     {
         hero.Attack += Attack;
     }

     public void Discard(IHero hero)
     {
         hero.Attack -= Attack;
     }
}