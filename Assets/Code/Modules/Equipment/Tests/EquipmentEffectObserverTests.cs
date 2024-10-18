using System;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class EquipmentEffectObserverTests
{
    #region STATICS
    
    private static readonly InventoryItem SWORD_ITEM_PLAIN = new InventoryItem()
    {
        Name = "Sword",
        Flags = ItemFlags.Equippable,
        IsOccupySingleEquipmentSlot = true,
        EquipmentSlots = new EquipmentSlotType[1] {EquipmentSlotType.RightHand}
    };
    
    private static readonly InventoryItem SWORD_ITEM_BUFF_ATTACK = new InventoryItem()
    {
        Name = "Sword",
        Flags = ItemFlags.Equippable,
        IsOccupySingleEquipmentSlot = true,
        EquipmentSlots = new EquipmentSlotType[1] {EquipmentSlotType.RightHand},
        ItemComponents = new List<IItemComponent>{
            new InventoryItem_AttackEquipComponent()
            {
                Attack = 5
            }
        }
    };
    
    private static readonly InventoryItem TWO_HANDED_SWORD_ITEM_PLAIN = new InventoryItem()
    {
        Name = "Two-handed sword",
        Flags = ItemFlags.Equippable,
        IsOccupySingleEquipmentSlot = false,
        EquipmentSlots = new EquipmentSlotType[2] {EquipmentSlotType.RightHand, EquipmentSlotType.LeftHand}
    };
    
    private static readonly InventoryItem TWO_HANDED_SWORD_ITEM_BUFF_ATTACK = new InventoryItem()
    {
        Name = "Two-handed sword",
        Flags = ItemFlags.Equippable,
        IsOccupySingleEquipmentSlot = false,
        EquipmentSlots = new EquipmentSlotType[2] {EquipmentSlotType.RightHand, EquipmentSlotType.LeftHand},
        ItemComponents = new List<IItemComponent>{
            new InventoryItem_AttackEquipComponent()
            {
                Attack = 10
            }
        }
    };
    
    private static readonly EquipmentSlotType[] DEFAULT_EQUIPMENT_SLOTS =
    {
        EquipmentSlotType.Head,
        EquipmentSlotType.Legs,
        EquipmentSlotType.Torso,
        EquipmentSlotType.LeftHand,
        EquipmentSlotType.RightHand
    };
    
    #endregion Statics
    
    [Test]
    public void EquipmentEffectObserverTest()
    {
        //Arrange:
        var equipment = new Equipment(DEFAULT_EQUIPMENT_SLOTS);
        HeroMock hero = new HeroMock();
        
        //Act:
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        
        //Assert:
        Assert.IsNotNull(equipmentEffectObserver);
    } 
    
    [Test]
    public void WhenConstructWithNullArgumentsThenException()
    {
        //Arrange:
        var equipment = new Equipment(DEFAULT_EQUIPMENT_SLOTS);
        HeroMock hero = new HeroMock();
        
        //Act:
        //Assert:
        Assert.Catch<Exception>(() =>
        {
            var observerWithNullEquipment = new EquipmentEffectObserver(null, hero);
        });
        Assert.Catch<Exception>(() =>
        {
            var observerWithNullHero = new EquipmentEffectObserver(equipment, null);
        });
        Assert.Catch<Exception>(() =>
        {
            var observerWithNullArguments = new EquipmentEffectObserver(null, null);
        });
    } 
    
    [Test]
    public void WhenEquipSingleSlotItemApplyEffect()
    {
        //Arrange:
        var equipment = new Equipment(DEFAULT_EQUIPMENT_SLOTS);
        HeroMock hero = new HeroMock()
        {
            Attack = 10
        };
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        
        //Act:
        int baseAttack = hero.Attack;
        SWORD_ITEM_BUFF_ATTACK.TryGetComponent<InventoryItem_AttackEquipComponent>(out var attackComponent);
        int attackBuff = attackComponent.Attack;
        equipment.TryEquipItem(SWORD_ITEM_BUFF_ATTACK);
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack + attackBuff, resultAttack);
    }
    
    [Test]
    public void WhenEquipMultiSlotItemApplyEffect()
    {
        //Arrange:
        var equipment = new Equipment(DEFAULT_EQUIPMENT_SLOTS);
        HeroMock hero = new HeroMock()
        {
            Attack = 10
        };
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        
        //Act:
        int baseAttack = hero.Attack;
        TWO_HANDED_SWORD_ITEM_BUFF_ATTACK.TryGetComponent<InventoryItem_AttackEquipComponent>(out var attackComponent);
        int attackBuff = attackComponent.Attack;
        equipment.TryEquipItem(TWO_HANDED_SWORD_ITEM_BUFF_ATTACK);
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack + attackBuff, resultAttack);
    }
    
    [Test]
    public void WhenUnequipItemDiscardEffect()
    {
        //Arrange:
        var equipment = new Equipment(DEFAULT_EQUIPMENT_SLOTS);
        HeroMock hero = new HeroMock()
        {
            Attack = 10
        };
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        
        //Act:
        int baseAttack = hero.Attack;
        equipment.TryEquipItem(SWORD_ITEM_BUFF_ATTACK);
        equipment.TryUnequipItem(SWORD_ITEM_BUFF_ATTACK);
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack, resultAttack);
    }
    
    [Test]
    public void WhenUnequipMultiSlotItemDiscardEffect()
    {
        //Arrange:
        var equipment = new Equipment(DEFAULT_EQUIPMENT_SLOTS);
        HeroMock hero = new HeroMock()
        {
            Attack = 10
        };
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        
        //Act:
        int baseAttack = hero.Attack;
        equipment.TryEquipItem(TWO_HANDED_SWORD_ITEM_BUFF_ATTACK);
        equipment.TryUnequipItem(TWO_HANDED_SWORD_ITEM_BUFF_ATTACK);
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack, resultAttack);
    }
    
    [Test]
    public void WhenEquipItemInPlaceOfEquippedItemApplyBuffsCorrectly()
    {
        //Arrange:
        var equipment = new Equipment(DEFAULT_EQUIPMENT_SLOTS);
        HeroMock hero = new HeroMock()
        {
            Attack = 10
        };
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        
        //Act:
        int baseAttack = hero.Attack;
        equipment.TryEquipItem(SWORD_ITEM_BUFF_ATTACK);
        equipment.TryEquipItem(TWO_HANDED_SWORD_ITEM_BUFF_ATTACK);
        TWO_HANDED_SWORD_ITEM_BUFF_ATTACK.TryGetComponent<InventoryItem_AttackEquipComponent>(out var attackComponent);
        int attackBuff = attackComponent.Attack;
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack + attackBuff, resultAttack);
    }
    
    [Test]
    public void WhenEquipPlainItemInPlaceOfEquippedItemDiscardBuffsCorrectly()
    {
        //Arrange:
        var equipment = new Equipment(DEFAULT_EQUIPMENT_SLOTS);
        HeroMock hero = new HeroMock()
        {
            Attack = 10
        };
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        
        //Act:
        int baseAttack = hero.Attack;
        equipment.TryEquipItem(TWO_HANDED_SWORD_ITEM_BUFF_ATTACK);
        equipment.TryEquipItem(SWORD_ITEM_PLAIN);
        TWO_HANDED_SWORD_ITEM_BUFF_ATTACK.TryGetComponent<InventoryItem_AttackEquipComponent>(out var attackComponent);
        int attackBuff = attackComponent.Attack;
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack, resultAttack);
    }
    
    [Test]
    public void WhenEquipItemWithEffectableComponentDontApplyBuffs()
    {
        //Arrange:
        var equipment = new Equipment(DEFAULT_EQUIPMENT_SLOTS);
        HeroMock hero = new HeroMock()
        {
            MaxHitPoints = 10
        };
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        InventoryItem charmOfHealth = new InventoryItem()
        {
            Name = "Charm",
            Flags = ItemFlags.Equippable | ItemFlags.Effectible,
            IsOccupySingleEquipmentSlot = true,
            EquipmentSlots = new EquipmentSlotType[1] {EquipmentSlotType.RightHand},
            ItemComponents = new List<IItemComponent>{
                new InventoryItem_HealthEffectComponent()
                {
                    HitPoints = 10
                }
            }
        };
        
        //Act:
        int baseHealth = hero.MaxHitPoints;
        equipment.TryEquipItem(charmOfHealth);
        int resultHealth = hero.MaxHitPoints;
        
        //Assert:
        Assert.AreEqual(baseHealth, resultHealth);
    }
}