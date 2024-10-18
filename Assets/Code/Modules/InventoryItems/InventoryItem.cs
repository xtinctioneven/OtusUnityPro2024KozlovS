using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class InventoryItem
{
    public string Name;
    
    //Meta Data
    public string Description;
    public Sprite Icon;
    
    public ItemFlags Flags;
    [ShowIf("@(Flags & ItemFlags.Equippable) == ItemFlags.Equippable")]
    public bool IsOccupySingleEquipmentSlot = true;
    [ShowIf("@(Flags & ItemFlags.Equippable) == ItemFlags.Equippable")]
    public EquipmentSlotType[] EquipmentSlots;
    [SerializeReference]
    public List<IItemComponent> ItemComponents = new List<IItemComponent>();

    public IReadOnlyList<IItemComponent> GetComponents()
    {
        return ItemComponents;
    }

    public bool TryGetComponent<T>(out T component)
    {
        foreach (IItemComponent itemComponent in ItemComponents)
        {
            if (itemComponent is T tcomponent)
            {
                component = tcomponent;
                return true;
            }
        }
        component = default;
        return false;
    }

    public InventoryItem Clone()
    {
        return new InventoryItem()
        {
            Name = Name,
            Description = Description,
            Icon = Icon,
            ItemComponents = CloneComponents(),
            Flags = Flags,
            IsOccupySingleEquipmentSlot = IsOccupySingleEquipmentSlot,
            EquipmentSlots = EquipmentSlots
        };
    }

    public List<IItemComponent> CloneComponents()
    {
        var list = new List<IItemComponent>();
        foreach (IItemComponent itemComponent in ItemComponents)
        {
            list.Add(itemComponent.Clone());
        }
        return list;
    }
}