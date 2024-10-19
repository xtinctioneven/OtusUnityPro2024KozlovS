using System;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class EquipmentEffectObserverTests
{
    private readonly EquipmentTestUtils _utils = new EquipmentTestUtils();
    
    [Test]
    public void WhenConstructObserver_AndAnyArgumentIsNull_ThenException()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
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
    public void WhenEquipSingleSlotItem_AndEquipmentEmpty_ThenApplyEffect()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        HeroMock hero = _utils.GetHeroMock(attack: 10);
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        var sword = _utils.GetSwordBuffAttackItem();
        
        //Act:
        int baseAttack = hero.Attack;
        sword.TryGetComponent<InventoryItem_AttackEquipComponent>(out var attackComponent);
        int attackBuff = attackComponent.Attack;
        equipment.TryEquipItem(sword);
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack + attackBuff, resultAttack);
    }
    
    [Test]
    public void WhenEquipMultiSlotItem_AndEquipmentEmpty_ThenApplyEffect()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        HeroMock hero = _utils.GetHeroMock(attack: 10);
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        var twoHandedSword = _utils.GetTwoHandedSwordBuffAttackItem();
        
        //Act:
        int baseAttack = hero.Attack;
        twoHandedSword.TryGetComponent<InventoryItem_AttackEquipComponent>(out var attackComponent);
        int attackBuff = attackComponent.Attack;
        equipment.TryEquipItem(twoHandedSword);
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack + attackBuff, resultAttack);
    }
    
    [Test]
    public void WhenUnequipItem_AndItemBuffsAttack_ThenDiscardEffect()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        HeroMock hero = _utils.GetHeroMock(attack: 10);
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        var sword = _utils.GetSwordBuffAttackItem();
        
        //Act:
        int baseAttack = hero.Attack;
        equipment.TryEquipItem(sword);
        equipment.TryUnequipItem(sword);
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack, resultAttack);
    }
    
    [Test]
    public void WhenUnequipMultiSlotItem_AndItemBuffsAttack_DiscardEffect()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        HeroMock hero = _utils.GetHeroMock(attack: 10);
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        var twoHandedSword = _utils.GetTwoHandedSwordBuffAttackItem();
        
        //Act:
        int baseAttack = hero.Attack;
        equipment.TryEquipItem(twoHandedSword);
        equipment.TryUnequipItem(twoHandedSword);
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack, resultAttack);
    }
    
    [Test]
    public void WhenEquipItem_AndSlotIsOccupied_ThenResolveBuffsCorrectly()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        HeroMock hero = _utils.GetHeroMock(attack: 10);
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        var twoHandedSword = _utils.GetTwoHandedSwordBuffAttackItem();
        var sword = _utils.GetSwordBuffAttackItem();
        
        //Act:
        int baseAttack = hero.Attack;
        equipment.TryEquipItem(sword);
        equipment.TryEquipItem(twoHandedSword);
        twoHandedSword.TryGetComponent<InventoryItem_AttackEquipComponent>(out var attackComponent);
        int attackBuff = attackComponent.Attack;
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack + attackBuff, resultAttack);
    }
    
    [Test]
    public void WhenEquipPlainItem_AndSlotIsOccupied_ThenDiscardBuffsCorrectly()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        HeroMock hero = _utils.GetHeroMock(attack: 10);
        var equipmentEffectObserver = new EquipmentEffectObserver(equipment, hero);
        equipmentEffectObserver.Construct();
        var twoHandedSword = _utils.GetTwoHandedSwordBuffAttackItem();
        var sword = _utils.GetSwordPlainItem();
        
        //Act:
        int baseAttack = hero.Attack;
        equipment.TryEquipItem(twoHandedSword);
        equipment.TryEquipItem(sword);
        int resultAttack = hero.Attack;
        
        //Assert:
        Assert.AreEqual(baseAttack, resultAttack);
    }
    
    [Test]
    public void WhenEquipItem_AndItemIsEffectible_ThenDontApplyEffects()
    {
        //Arrange:
        var equipment = _utils.GetDefaultEquipment();
        HeroMock hero = _utils.GetHeroMock(attack: 10);
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