using System;
using System.Linq;
using NUnit.Framework;

[TestFixture]
public class EquipmentTests 
{
    private EquipmentTestUtils _utils = new EquipmentTestUtils();
    
    [Test]
    public void WhenConstructEquipment_AndPassedSlotsIsNull_ThenConstructEmptyEquipment()
    {
        //Arrange:
        
        //Act:
        var equipment = new Equipment(null);
        var equipmentKeys = equipment.GetEquipmentSlots();
        
        //Assert:
        Assert.NotNull(equipment);
        Assert.AreEqual(equipmentKeys.Count(), 0);
    }
    
    [Test]
    public void WhenConstructEquipment_AndPassedSlotsHaveDuplicates_ThenDropDuplicateSlots()
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
        Assert.AreEqual(_utils.GetDefaultEquipmentSlotTypes(), equipmentKeys);
    }
    
    [Test]
    public void WhenTryEquipItem_AndEquipmentIsEmpty_ThenSuccess()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        var swordItem = _utils.GetSwordPlainItem();
        
        //Act:
        bool success = equipment.TryEquipItem(swordItem);
        equipment.TryFindItem(swordItem, out var equipmentItem);
        var itemInRightHand = equipment.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.AreEqual(itemInRightHand, swordItem);
        Assert.AreEqual(equipmentItem, swordItem);
    }
    
    [Test]
    public void WhenTryFindItem_AndItemIsPresent_ThenSuccess()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        var swordItem = _utils.GetSwordPlainItem();
        equipment.TryEquipItem(swordItem);
        
        //Act:
        bool success = equipment.TryFindItem(swordItem, out var equipmentItem);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.AreEqual(equipmentItem, swordItem);
    }
    
    [Test]
    public void WhenTryFindItem_AndItemIsMultiSlot_ThenSuccess()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        var twoHandedSword = _utils.GetTwoHandedSwordPlainItem();
        equipment.TryEquipItem(twoHandedSword);
        
        //Act:
        bool success = equipment.TryFindItem(twoHandedSword, out var equipmentItem);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.AreEqual(equipmentItem, twoHandedSword);
    }

    [Test]
    public void WhenTryFindItem_AndItemIsNull_ThenException()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        
        //Act:
        //Assert:
        Assert.Catch<ArgumentNullException>(() =>
        {
            equipment.TryFindItem(null, out _);
        }, "Can't find null items");
    }
    
    [Test]
    public void WhenTryGetItemInSlot_AndItemIsPresent_ThenSuccess()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        var swordItem = _utils.GetSwordPlainItem();
        equipment.TryEquipItem(swordItem);
        
        //Act:
        InventoryItem itemInRightHand = equipment.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.AreEqual(swordItem, itemInRightHand);
    }
    
    [Test]
    public void WhenTryGetItemInSlot_AndItemIsMultiSlot_ThenSuccess()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        var twoHandedSword = _utils.GetTwoHandedSwordPlainItem();
        equipment.TryEquipItem(twoHandedSword);
        
        //Act:
        InventoryItem itemInLeftHand = equipment.GetItemInSlot(EquipmentSlotType.LeftHand);
        InventoryItem itemInRightHand = equipment.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.AreEqual(twoHandedSword, itemInLeftHand);
        Assert.AreEqual(twoHandedSword, itemInRightHand);
        Assert.AreEqual(itemInRightHand, itemInLeftHand);
    }
    
    [Test]
    public void WhenTryUnequipItem_AndItemIsPresent_ThenSuccess()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        var swordItem = _utils.GetSwordPlainItem();
        equipment.TryEquipItem(swordItem);
        
        //Act:
        bool success = equipment.TryUnequipItem(swordItem);
        equipment.TryFindItem(swordItem, out var equipmentItem);
        var itemInRightHand = equipment.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.IsNull(equipmentItem);
        Assert.IsNull(itemInRightHand);
    }
    
    [Test]
    public void WhenTryEquipItem_AndItemIsNull_ThenSuccess()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        
        //Act:
        Assert.Catch<ArgumentNullException>(() =>
        {
            equipment.TryEquipItem(null);
        }, "Can't equip empty item");
    }
    
    [Test]
    public void WhenTryEquipItem_AndSlotIsAbsent_ThenFail()
    {
        //Arrange:
        Equipment bodyEquipment = new Equipment( new EquipmentSlotType[]
        {
            EquipmentSlotType.Head,
            EquipmentSlotType.Torso,
            EquipmentSlotType.Legs
        });
        var swordItem = _utils.GetSwordPlainItem();
        
        //Act:
        bool success = bodyEquipment.TryEquipItem(swordItem);
        bool isSwordInEquipment = bodyEquipment.TryFindItem(swordItem, out var equipmentItem);
        var itemInRightHand = bodyEquipment.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.IsFalse(success);
        Assert.IsFalse(isSwordInEquipment);
        Assert.AreNotEqual(equipmentItem, swordItem);
        Assert.AreNotEqual(itemInRightHand, swordItem);
    }
    
    [Test]
    public void WhenTryEquipItem_AndItemHaveNoSlots_ThenException()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
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
            equipment.TryEquipItem(axeItem);
        }, "Can't equip item with zero slots");
    }
    
    [Test]
    public void WhenTryUnequipItem_AndItemIsNull_ThenException()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        
        //Act:
        //Assert:
        Assert.Catch<ArgumentNullException>(() =>
        {
            equipment.TryUnequipItem(null);
        }, "Can't unequip empty item");
    }

    [Test]
    public void WhenTryUnequipItem_AndItemIsAbsent_ThenFail()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        var swordItem = _utils.GetSwordPlainItem();
        
        //Act:
        var success = equipment.TryUnequipItem(swordItem);
        
        //Assert:
        Assert.IsFalse(success);
    }
    
    [Test]
    public void WhenTryEquipItem_AndSlotIsOccupied_ThenSwapItem()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        InventoryItem axeItem = new InventoryItem()
        {
            Name = "TestAxe",
            Flags = ItemFlags.Equippable,
            IsOccupySingleEquipmentSlot = true,
            EquipmentSlots = new EquipmentSlotType[] {EquipmentSlotType.RightHand}
        };
        var swordItem = _utils.GetSwordPlainItem();
        equipment.TryEquipItem(swordItem);
        
        //Act:
        bool success = equipment.TryEquipItem(axeItem);
        bool isSwordInEquipment = equipment.TryFindItem(swordItem, out var equipmentItem);
        var itemInRightHand = equipment.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.IsFalse(isSwordInEquipment);
        Assert.IsNull(equipmentItem);
        Assert.AreEqual(itemInRightHand, axeItem);
    }
    
    [Test]
    public void WhenTryEquipItem_AndItemIsMultiSlot_ThenOccupyAllItsSlots()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        var twoHandedSword = _utils.GetTwoHandedSwordPlainItem();
        
        //Act:
        bool success = equipment.TryEquipItem(twoHandedSword);
        bool isSwordInEquipment = equipment.TryFindItem(twoHandedSword, out var equipmentItem);
        var itemInRightHand = equipment.GetItemInSlot(EquipmentSlotType.RightHand);
        var itemInLeftHand = equipment.GetItemInSlot(EquipmentSlotType.LeftHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.IsTrue(isSwordInEquipment);
        Assert.AreEqual(equipmentItem, twoHandedSword);
        Assert.AreEqual(itemInRightHand, twoHandedSword);
        Assert.AreEqual(itemInLeftHand, twoHandedSword);
        Assert.AreEqual(itemInLeftHand, itemInRightHand);
    }
    
    [Test]
    public void WhenTryUnequipItem_AndItemIsMultiSlot_ThenEmptyAllItsSlots()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        var twoHandedSword = _utils.GetTwoHandedSwordPlainItem();
        equipment.TryEquipItem(twoHandedSword);
        
        //Act:
        bool success = equipment.TryUnequipItem(twoHandedSword);
        bool isSwordInEquipment = equipment.TryFindItem(twoHandedSword, out var equipmentItem);
        var itemInRightHand = equipment.GetItemInSlot(EquipmentSlotType.RightHand);
        var itemInLeftHand = equipment.GetItemInSlot(EquipmentSlotType.LeftHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.IsFalse(isSwordInEquipment);
        Assert.AreNotEqual(equipmentItem, twoHandedSword);
        Assert.IsNull(itemInRightHand);
        Assert.IsNull(itemInLeftHand);
    }
    
    [Test]
    public void WhenTryEquipItem_AndItemIsMultiSlotWithSingleSlot_ThenSuccessWithWarning()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        InventoryItem twoHandedSwordSingleSlot = new InventoryItem()
        {
            Name = "Two-handed sword with single equipment slot",
            Flags = ItemFlags.Equippable,
            IsOccupySingleEquipmentSlot = false,
            EquipmentSlots = new EquipmentSlotType[] {EquipmentSlotType.RightHand}
        };
        
        //Act:
        bool success = equipment.TryEquipItem(twoHandedSwordSingleSlot);
        bool isSwordInEquipment = equipment.TryFindItem(twoHandedSwordSingleSlot, out var equipmentItem);
        var itemInRightHand = equipment.GetItemInSlot(EquipmentSlotType.RightHand);
        
        //Assert:
        Assert.IsTrue(success);
        Assert.IsTrue(isSwordInEquipment);
        Assert.AreEqual(equipmentItem, twoHandedSwordSingleSlot);
        Assert.AreEqual(itemInRightHand, twoHandedSwordSingleSlot);
    }
    
    [Test]
    public void WhenTryEquipItem_AndItemIsNotEquippable_ThenFail()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        InventoryItem manaPotion = new InventoryItem()
        {
            Name = "Mana potion",
            Flags = ItemFlags.Consumable
        };
        
        //Act:
        bool success = equipment.TryEquipItem(manaPotion);
        bool isItemInEquipment = equipment.TryFindItem(manaPotion, out var equipmentItem);
        
        //Assert:
        Assert.IsFalse(success);
        Assert.IsFalse(isItemInEquipment);
        Assert.IsNull(equipmentItem);
    }
}