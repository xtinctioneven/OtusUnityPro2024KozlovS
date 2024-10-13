using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Inventory
{
    public event Action<InventoryItem> OnAdded;
    public event Action<InventoryItem> OnRemoved;
    public event Action<InventoryItem> OnConsumed;
    
    [ShowInInspector] public Dictionary<InventoryItem, int> _inventory = new();
    
    [HideInPlayMode] public EquipmentSlotType[] EquipmentSlots;
    public Equipment Equipment;

    public void ConsumeItem(InventoryItem otherItem)
    {
        if (TryFindItem(otherItem, out var item))
        {
            if ((item.Flags & ItemFlags.Consumable) == ItemFlags.Consumable)
            {
                Remove(item);
                OnConsumed?.Invoke(item);
            }
            else
            {
                Debug.LogWarning($"Item {item.Name} is not consumable!");
            }
        }
    }
    
    public void AddItem(InventoryItem item)
    {
        if (CanAddItem(item))
        {
            _inventory[item] = 1;
            OnAdded?.Invoke(item);
        }
        else
        {
            Debug.Log($"Item {item.Name} is already added");
        }
    }

    private bool CanAddItem(InventoryItem item)
    {
        foreach (var inventoryPair in _inventory)
        {
            if (IsEqualItems(inventoryPair.Key, item))
            {
                return false;
            }   
        }
        return true;
    }

    public void RemoveItem(InventoryItem otherItem)
    {
        if (TryFindItem(otherItem, out var item))
        {
            Remove(item);
        }
    }

    private void Remove(InventoryItem item)
    {
        _inventory.Remove(item);
        OnRemoved?.Invoke(item);
    }

    public bool TryFindItem(InventoryItem otherItem, out InventoryItem item)
    {
        foreach (var inventoryPair in _inventory)
        {
            if (IsEqualItems(inventoryPair.Key, otherItem))
            {
                item = inventoryPair.Key;
                return true;
            }
        }
        item = default;
        return false;
    }

    private bool IsEqualItems(InventoryItem item, InventoryItem otherItem)
    {
        return item.Name == otherItem.Name;
    }
}