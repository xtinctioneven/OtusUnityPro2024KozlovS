using System;

[Serializable]
public class InventoryItem_AttackEffectComponent : IItemComponent, IEquippableComponent
{
     public int Attack = 5;
     
     public IItemComponent Clone()
     {
         return new InventoryItem_AttackEffectComponent()
         {
             Attack = Attack,
         };
     }
     
     public void Apply(Hero hero)
     {
         hero.Attack += Attack;
     }

     public void Discard(Hero hero)
     {
         hero.Attack -= Attack;
     }
}