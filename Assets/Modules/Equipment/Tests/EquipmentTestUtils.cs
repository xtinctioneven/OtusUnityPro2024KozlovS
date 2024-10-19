using System.Collections.Generic;

public class EquipmentTestUtils
{
    public HeroMock GetHeroMock(int maxHitpoints = 10, int mana = 10, int attack = 10)
    {
        return new HeroMock()
        {
            MaxHitPoints = maxHitpoints,
            Mana = mana,
            Attack = attack
        };
    }

    public EquipmentSlotType[] GetDefaultEquipmentSlotTypes()
    {
        return new EquipmentSlotType[5]
        {
            EquipmentSlotType.Head,
            EquipmentSlotType.Legs,
            EquipmentSlotType.Torso,
            EquipmentSlotType.LeftHand,
            EquipmentSlotType.RightHand
        };
    }

    public Equipment GetDefaultEquipment()
    {
        return new Equipment(GetDefaultEquipmentSlotTypes());
    }

    public InventoryItem GetSwordPlainItem()
    {
        return new InventoryItem()
        {
            Name = "Sword",
            Flags = ItemFlags.Equippable,
            IsOccupySingleEquipmentSlot = true,
            EquipmentSlots = new EquipmentSlotType[1] { EquipmentSlotType.RightHand }
        };
    }

    public InventoryItem GetSwordBuffAttackItem()
    {
        return new InventoryItem()
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
    }

    public InventoryItem GetTwoHandedSwordPlainItem()
    {
        return new InventoryItem()
        {
            Name = "Two-handed sword",
            Flags = ItemFlags.Equippable,
            IsOccupySingleEquipmentSlot = false,
            EquipmentSlots = new EquipmentSlotType[2] {EquipmentSlotType.RightHand, EquipmentSlotType.LeftHand}
        };
    }

    public InventoryItem GetTwoHandedSwordBuffAttackItem()
    {
        return new InventoryItem()
        {
            Name = "Sword",
            Flags = ItemFlags.Equippable,
            IsOccupySingleEquipmentSlot = true,
            EquipmentSlots = new EquipmentSlotType[1] { EquipmentSlotType.RightHand },
            ItemComponents = new List<IItemComponent>{
                new InventoryItem_AttackEquipComponent()
                {
                    Attack = 10
                }
            }
        };
    }
}
