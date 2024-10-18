using System;
using System.Linq;
using NUnit.Framework;

[TestFixture]
public class EquipmentTests 
{
    #region STATICS
    private static readonly InventoryItem SWORD_ITEM = new InventoryItem()
        {
            Name = "Sword",
            Flags = ItemFlags.Equippable,
            IsOccupySingleEquipmentSlot = true,
            EquipmentSlots = new EquipmentSlotType[1] {EquipmentSlotType.RightHand}
        };
    
    private static readonly InventoryItem TWO_HANDED_SWORD_ITEM = new InventoryItem()
        {
            Name = "Two-handed sword",
            Flags = ItemFlags.Equippable,
            IsOccupySingleEquipmentSlot = false,
            EquipmentSlots = new EquipmentSlotType[2] {EquipmentSlotType.RightHand, EquipmentSlotType.LeftHand}
        };
    
    private static readonly EquipmentSlotType[] DEFAULT_EQUIPMENT_SLOTS =
    {
        EquipmentSlotType.Head,
        EquipmentSlotType.Legs,
        EquipmentSlotType.Torso,
        EquipmentSlotType.LeftHand,
        EquipmentSlotType.RightHand
    };

    private static readonly Equipment EQUIPMENT = new Equipment(DEFAULT_EQUIPMENT_SLOTS); 
    
    #endregion STATICS
    
    [Test]
    public void EquipmentConstructorTest()
    {
        //Arrange:
        EquipmentSlotType[] equipmentSlots =
        {
            EquipmentSlotType.Head,
            EquipmentSlotType.Legs,
            EquipmentSlotType.Torso,
            EquipmentSlotType.LeftHand,
            EquipmentSlotType.RightHand
        };
        
        //Act:
        var equipment = new Equipment(equipmentSlots);
        var equipmentKeys = equipment.GetEquipmentSlots();
        
        //Assert:
        Assert.IsNotNull(equipment);
        Assert.AreEqual(equipmentSlots, equipmentKeys);
    }
    
    [Test]
    public void ConstructorWhenEquipmentSlotsIsNullThenConstructEmptyEquipment()
    {
        //Arrange:
        EquipmentSlotType[] equipmentSlots = null;
        
        //Act:
        var equipment = new Equipment(equipmentSlots);
        var equipmentKeys = equipment.GetEquipmentSlots();
        
        //Assert:
        Assert.NotNull(equipment);
        Assert.AreEqual(equipmentKeys.Count(), 0);
    }
    
    [Test]
    public void ConstructorWhenPassedDuplicateSlotsThenDropDuplicateSlots()
    {
        //Arrange:
        EquipmentSlotType[] equipmentSlotsDuplicated =
        {
            EquipmentSlotType.Head,
            EquipmentSlotType.Legs,
            EquipmentSlotType.Torso,
            EquipmentSlotType.LeftHand,
            EquipmentSlotType.RightHand,
            EquipmentSlotType.Head,
            EquipmentSlotType.Legs,
            EquipmentSlotType.Torso,
            EquipmentSlotType.LeftHand,
            EquipmentSlotType.RightHand
        };
        
        //Act:
        var equipment = new Equipment(equipmentSlotsDuplicated);
        var equipmentKeys = equipment.GetEquipmentSlots();
        
        //Assert:
        Assert.IsNotNull(equipment);
        Assert.AreEqual(DEFAULT_EQUIPMENT_SLOTS, equipmentKeys);
    }
    
    [Test]
    public void EquipItemTest()
    {
        //Arrange:
        
        //Act:
        bool success = EQUIPMENT.TryEquipItem(SWORD_ITEM);
        EQUIPMENT.TryFindItem(SWORD_ITEM, out var equipmentItem);
        var itemInRightHand = EQUIPMENT.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.AreEqual(itemInRightHand, SWORD_ITEM);
        Assert.AreEqual(equipmentItem, SWORD_ITEM);
    }
    
    [Test]
    public void TryFindItemTest()
    {
        //Arrange:
        EQUIPMENT.TryEquipItem(SWORD_ITEM);
        
        //Act:
        bool success = EQUIPMENT.TryFindItem(SWORD_ITEM, out var equipmentItem);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.AreEqual(equipmentItem, SWORD_ITEM);
    }
    
    [Test]
    public void TryFindMultiSlotItemTest()
    {
        //Arrange:
        EQUIPMENT.TryEquipItem(TWO_HANDED_SWORD_ITEM);
        
        //Act:
        bool success = EQUIPMENT.TryFindItem(TWO_HANDED_SWORD_ITEM, out var equipmentItem);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.AreEqual(equipmentItem, TWO_HANDED_SWORD_ITEM);
    }

    [Test]
    public void WhenTryFindNullItemThenException()
    {
        //Arrange:
        
        //Act:
        //Assert:
        Assert.Catch<ArgumentNullException>(() =>
        {
            EQUIPMENT.TryFindItem(null, out _);
        }, "Can't find null items");
    }
    
    [Test]
    public void GetItemInSlotTest()
    {
        //Arrange:
        EQUIPMENT.TryEquipItem(SWORD_ITEM);
        
        //Act:
        InventoryItem itemInRightHand = EQUIPMENT.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.AreEqual(SWORD_ITEM, itemInRightHand);
    }
    
    [Test]
    public void GetMultiSlotItemInSlotTest()
    {
        //Arrange:
        EQUIPMENT.TryEquipItem(TWO_HANDED_SWORD_ITEM);
        
        //Act:
        InventoryItem itemInLeftHand = EQUIPMENT.GetItemInSlot(EquipmentSlotType.LeftHand);
        InventoryItem itemInRightHand = EQUIPMENT.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.AreEqual(TWO_HANDED_SWORD_ITEM, itemInLeftHand);
        Assert.AreEqual(TWO_HANDED_SWORD_ITEM, itemInRightHand);
        Assert.AreEqual(itemInRightHand, itemInLeftHand);
    }
    
    [Test]
    public void UnequipItemTest()
    {
        //Arrange:
        
        //Act:
        EQUIPMENT.TryEquipItem(SWORD_ITEM);
        bool success = EQUIPMENT.TryUnequipItem(SWORD_ITEM);
        EQUIPMENT.TryFindItem(SWORD_ITEM, out var equipmentItem);
        var itemInRightHand = EQUIPMENT.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.IsNull(equipmentItem);
        Assert.IsNull(itemInRightHand);
    }
    
    [Test]
    public void WhenTryEquipNullItemThenException()
    {
        //Arrange:
        
        //Act:
        Assert.Catch<ArgumentNullException>(() =>
        {
            EQUIPMENT.TryEquipItem(null);
        }, "Can't equip empty item");
    }
    
    [Test]
    public void WhenTryEquipItemInAbsentSlotThenFail()
    {
        //Arrange:
        Equipment bodyEquipment = new Equipment( new EquipmentSlotType[3]
        {
            EquipmentSlotType.Head,
            EquipmentSlotType.Torso,
            EquipmentSlotType.Legs
        });
        
        //Act:
        bool success = bodyEquipment.TryEquipItem(SWORD_ITEM);
        bool isSwordInEquipment = bodyEquipment.TryFindItem(SWORD_ITEM, out var equipmentItem);
        var itemInRightHand = bodyEquipment.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.IsFalse(success);
        Assert.IsFalse(isSwordInEquipment);
        Assert.AreNotEqual(equipmentItem, SWORD_ITEM);
        Assert.AreNotEqual(itemInRightHand, SWORD_ITEM);
    }
    
    [Test]
    public void WhenTryEquipItemWithoutEquipmentSlotsThenException()
    {
        //Arrange:
        InventoryItem axeItem = new InventoryItem()
        {
            Name = "TestAxe",
            Flags = ItemFlags.Equippable,
            IsOccupySingleEquipmentSlot = true,
            EquipmentSlots = Array.Empty<EquipmentSlotType>()
        };
        
        //Act:
        //Assert:
        Assert.Catch<ArgumentException>(() =>
        {
            EQUIPMENT.TryEquipItem(axeItem);
        }, "Can't equip item with zero slots");
    }
    
    [Test]
    public void WhenTryUnequipNullItemThenException()
    {
        //Arrange:
        
        //Act:
        //Assert:
        Assert.Catch<ArgumentNullException>(() =>
        {
            EQUIPMENT.TryUnequipItem(null);
        }, "Can't unequip empty item");
    }

    [Test]
    public void WhenTryUnequipUnequippedItemThenFail()
    {
        //Arrange:
        
        //Act:
        var success = EQUIPMENT.TryUnequipItem(SWORD_ITEM);
        
        //Assert:
        Assert.IsFalse(success);
    }
    
    [Test]
    public void WhenTryEquipItemInOccupiedSlotThenSuccess()
    {
        //Arrange:
        InventoryItem axeItem = new InventoryItem()
        {
            Name = "TestAxe",
            Flags = ItemFlags.Equippable,
            IsOccupySingleEquipmentSlot = true,
            EquipmentSlots = new EquipmentSlotType[1] {EquipmentSlotType.RightHand}
        };
        EQUIPMENT.TryEquipItem(SWORD_ITEM);
        
        //Act:
        bool success = EQUIPMENT.TryEquipItem(axeItem);
        bool isSwordInEquipment = EQUIPMENT.TryFindItem(SWORD_ITEM, out var equipmentItem);
        var itemInRightHand = EQUIPMENT.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.IsFalse(isSwordInEquipment);
        Assert.IsNull(equipmentItem);
        Assert.AreEqual(itemInRightHand, axeItem);
    }
    
    [Test]
    public void EquipMultiSlotItem()
    {
        //Arrange:
        
        //Act:
        bool success = EQUIPMENT.TryEquipItem(TWO_HANDED_SWORD_ITEM);
        bool isSwordInEquipment = EQUIPMENT.TryFindItem(TWO_HANDED_SWORD_ITEM, out var equipmentItem);
        var itemInRightHand = EQUIPMENT.GetItemInSlot(EquipmentSlotType.RightHand);
        var itemInLeftHand = EQUIPMENT.GetItemInSlot(EquipmentSlotType.LeftHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.IsTrue(isSwordInEquipment);
        Assert.AreEqual(equipmentItem, TWO_HANDED_SWORD_ITEM);
        Assert.AreEqual(itemInRightHand, TWO_HANDED_SWORD_ITEM);
        Assert.AreEqual(itemInLeftHand, TWO_HANDED_SWORD_ITEM);
        Assert.AreEqual(itemInLeftHand, itemInRightHand);
    }
    
    [Test]
    public void UnequipMultiSlotItem()
    {
        //Arrange:
        EQUIPMENT.TryEquipItem(TWO_HANDED_SWORD_ITEM);
        
        //Act:
        bool success = EQUIPMENT.TryUnequipItem(TWO_HANDED_SWORD_ITEM);
        bool isSwordInEquipment = EQUIPMENT.TryFindItem(TWO_HANDED_SWORD_ITEM, out var equipmentItem);
        var itemInRightHand = EQUIPMENT.GetItemInSlot(EquipmentSlotType.RightHand);
        var itemInLeftHand = EQUIPMENT.GetItemInSlot(EquipmentSlotType.LeftHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.IsFalse(isSwordInEquipment);
        Assert.AreNotEqual(equipmentItem, TWO_HANDED_SWORD_ITEM);
        Assert.IsNull(itemInRightHand);
        Assert.IsNull(itemInLeftHand);
    }
    
    [Test]
    public void WhenTryEquipMultiSlotItemWithSingleSlotThenSuccessWithWarning()
    {
        //Arrange:
        InventoryItem twoHandedSwordSingleSlot = new InventoryItem()
        {
            Name = "Two-handed sword",
            Flags = ItemFlags.Equippable,
            IsOccupySingleEquipmentSlot = false,
            EquipmentSlots = new EquipmentSlotType[1] {EquipmentSlotType.RightHand}
        };
        
        //Act:
        bool success = EQUIPMENT.TryEquipItem(TWO_HANDED_SWORD_ITEM);
        bool isSwordInEquipment = EQUIPMENT.TryFindItem(TWO_HANDED_SWORD_ITEM, out var equipmentItem);
        var itemInRightHand = EQUIPMENT.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.IsTrue(isSwordInEquipment);
        Assert.AreEqual(equipmentItem, TWO_HANDED_SWORD_ITEM);
        Assert.AreEqual(itemInRightHand, TWO_HANDED_SWORD_ITEM);
    }
    
    [Test]
    public void WhenTryEquipUnequippableItemThenFail()
    {
        //Arrange:
        InventoryItem manaPotion = new InventoryItem()
        {
            Name = "Mana potion",
            Flags = ItemFlags.Consumable
        };
        
        //Act:
        bool success = EQUIPMENT.TryEquipItem(manaPotion);
        bool isItemInEquipment = EQUIPMENT.TryFindItem(manaPotion, out var equipmentItem);
        
        //Assert:
        Assert.IsFalse(success);
        Assert.IsFalse(isItemInEquipment);
        Assert.IsNull(equipmentItem);
    }
}