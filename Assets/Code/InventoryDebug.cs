using System;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryDebug : MonoBehaviour
{
    public Hero Hero;
    public Inventory Inventory = new Inventory();
    private InventoryEffectObserver InventoryEffectObserver;
    private EquipmentEffectObserver EquipmentEffectObserver;
    
    [HideInPlayMode] public EquipmentSlotType[] EquipmentSlots;
    public Equipment Equipment;
    
    private void Awake()
    {
        Equipment = new Equipment(EquipmentSlots);
        Equipment.OnUnequipped += Inventory.AddItem;
        InventoryEffectObserver = new InventoryEffectObserver(Inventory, Hero);
        InventoryEffectObserver.Construct();
        EquipmentEffectObserver = new EquipmentEffectObserver(Equipment, Hero);
        EquipmentEffectObserver.Construct();
    }

    [FoldoutGroup("Inventory Debug")]
    [Button]
    public void AddItem(InventoryItemConfig itemConfig)
    {
        var item = itemConfig.Item.Clone();
        Inventory.AddItem(item);
    }
    
    [FoldoutGroup("Inventory Debug")]
    [Button]
    public void RemoveItem(InventoryItemConfig itemConfig)
    {
        var item = itemConfig.Item.Clone();
        Inventory.RemoveItem(item);
    }

    [FoldoutGroup("Inventory Debug")]
    [Button]
    public void ConsumeItem(InventoryItemConfig itemConfig)
    {
        var item = itemConfig.Item.Clone();
        Inventory.ConsumeItem(item);
    }

    [FoldoutGroup("Equipment Debug")]
    [Button]
    public void EquipItemFromInventory(InventoryItemConfig itemConfig)
    {
        var tempItem = itemConfig.Item.Clone();
        if (!Inventory.TryFindItem(tempItem, out var inventoryItem))
        {
            Debug.LogWarning($"Could not find an item with name {itemConfig.Item.Name}");
            return;
        }
        Equipment.TryEquipItem(inventoryItem);
        Inventory.RemoveItem(inventoryItem);
    }

    [FoldoutGroup("Equipment Debug")]
    [Button]
    public void EquipItemFromNew(InventoryItemConfig itemConfig)
    {
        var tempItem = itemConfig.Item.Clone();
        Equipment.TryEquipItem(tempItem);
    }

    [FoldoutGroup("Equipment Debug")]
    [Button]
    public void UnequipItem(InventoryItemConfig itemConfig)
    {
        var tempItem = itemConfig.Item.Clone();
        Equipment.TryUnequipItem(tempItem);
    }
}