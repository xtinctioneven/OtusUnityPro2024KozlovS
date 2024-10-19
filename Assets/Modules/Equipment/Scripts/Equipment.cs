using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Equipment
{
    public event Action<InventoryItem> OnEquipped;
    public event Action<InventoryItem> OnUnequipped;
    
    [ShowInInspector] private Dictionary<EquipmentSlotType, InventoryItem> _equipment = new();

    public Equipment(IEnumerable<EquipmentSlotType> equipmentSlotTypes)
    {
        if (equipmentSlotTypes == null)
        {
            Debug.LogWarning("No equipment slots were specified. Equipment will be empty!");
            return;
        }
        foreach (var equipmentSlot in equipmentSlotTypes)
        {
            _equipment.TryAdd(equipmentSlot, null);
        }
    }

    public IEnumerable<EquipmentSlotType> GetEquipmentSlots()
    {
        return _equipment.Keys;
    }

    public InventoryItem GetItemInSlot(EquipmentSlotType equipmentSlotType)
    {
        if (!_equipment.TryGetValue(equipmentSlotType, out var item))
        {
            Debug.Log($"There is no slot {equipmentSlotType} in target equipment.");
            return null;
        }
        return item;
    }
    
    public bool TryFindItem(InventoryItem tempItem, out InventoryItem equipmentItem)
    {
        if (tempItem == null)
        {
            throw new ArgumentNullException(nameof(tempItem));
        }
        foreach (var equipmentPair in _equipment)
        {
            if (equipmentPair.Value == null)
            {
                continue;
            }
            if (tempItem.Name == equipmentPair.Value.Name)
            {
                equipmentItem = equipmentPair.Value;
                return true;
            }
        }
        equipmentItem = default;
        return false;
    }
    
    public bool TryEquipItem(InventoryItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        
        if (CanEquip(item, out IEnumerable<EquipmentSlotType> fittingSlots))
        {
            if (item.IsOccupySingleEquipmentSlot)
            {
                EquipSingleSlot(item, fittingSlots);
            }
            else
            {
                EquipMultiSlot(item, fittingSlots);
            }
            return true;
        }
        return false;
    }

    public bool TryUnequipItem(InventoryItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        if (!TryFindItem(item, out InventoryItem equipmentItem))
        {
            Debug.LogWarning($"There is no \"{item.Name}\" in equipment!");
            return false;
        }
        foreach (var slot in equipmentItem.EquipmentSlots)
        {
            _equipment[slot] = null;
        }
        Unequip(equipmentItem);
        return true;
    }

    private void Unequip(InventoryItem inventoryItem)
    {
        OnUnequipped?.Invoke(inventoryItem);        
    }

    private bool CanEquip(InventoryItem item, out IEnumerable<EquipmentSlotType> fittingSlots)
    {
        if ((item.Flags & ItemFlags.Equippable) != ItemFlags.Equippable)
        {
            Debug.LogWarning($"\"{item.Name}\" is not equippable!");
            fittingSlots = default;
            return false;
        }
        if (item.EquipmentSlots == null || item.EquipmentSlots.Length == 0)
        {
            throw new ArgumentException($"Item \"{item.Name}\" has no equipment slots!");
        }
        if (item.IsOccupySingleEquipmentSlot)
        {
            //Предмет занимает один слот экипировки - одноручный меч, щит, шлем и т.д.
            return CanEquipSingleSlot(item, out fittingSlots);
        }
        else
        {
            //Предмет занимает несколько слотов экипировки - двуручный топор и др.
            return CanEquipMultiSlot(item, out fittingSlots);
        }
    }

    private bool CanEquipSingleSlot(InventoryItem item, out IEnumerable<EquipmentSlotType> fittingSlots)
    {
        var equipmentSlots = _equipment.Keys;
        var itemSlots = item.EquipmentSlots;
        var slotsList = new List<EquipmentSlotType>();
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (equipmentSlots.Contains(itemSlots[i]))
            {
                slotsList.Add(itemSlots[i]);
            }
        }
        if (!slotsList.Any())
        {
            Debug.LogWarning($"There is no equipment slot for \"{item.Name}\"!");
            fittingSlots = default;
            return false;
        }
        else
        {
            fittingSlots = slotsList;
            return true;
        }
    }

    private bool CanEquipMultiSlot(InventoryItem item, out IEnumerable<EquipmentSlotType> neededSlots)
    {
        var equipmentSlots = _equipment.Keys;
        var itemSlots = item.EquipmentSlots;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (!equipmentSlots.Contains(itemSlots[i]))
            {
                Debug.LogWarning($"Can't equip \"{item.Name}\"! There is no {itemSlots[i]} slot in the equipment!");
                neededSlots = default;
                return false;
            }
        }
        neededSlots = itemSlots;
        return true;
    }

    private void EquipSingleSlot(InventoryItem item, IEnumerable<EquipmentSlotType> fittingSlots)
    {
        //Пробегаемся по подходящим слотам, ищем первый пустой 
        for (int i = 0; i < fittingSlots.Count(); i++)
        {
            var slot = fittingSlots.ElementAt(i);
            if (_equipment[slot] == null)
            {
                _equipment[slot] = item;
                OnEquipped?.Invoke(item);
                return;
            }
        }
        //Все слоты заняты - экипируем предмет в первый подходящий слот
        UnequipSlot(fittingSlots.First());
        _equipment[fittingSlots.First()] = item;
        OnEquipped?.Invoke(item);
    }
    
    private void EquipMultiSlot(InventoryItem item, IEnumerable<EquipmentSlotType> neededSlots)
    {
        if (neededSlots.Count() == 1)
        {
            Debug.LogWarning($"Item \"{item.Name}\" marked as multi-slotted item but require only 1 slot!");
        }
        //Пробегаемся по необходимым слотам, снимаем с них экипировку 
        for (int i = 0; i < neededSlots.Count(); i++)
        {
            var slot = neededSlots.ElementAt(i);
            UnequipSlot(slot);
            _equipment[slot] = item;
        }
        OnEquipped?.Invoke(item);
    }

    private void UnequipSlot(EquipmentSlotType slot)
    {
        if (_equipment.ContainsKey(slot))
        {
            if (_equipment[slot] != null)
            {
                Unequip(_equipment[slot]);
            }
            _equipment[slot] = null;
        }
        else
        {
            Debug.LogWarning($"There is no {slot} slot in equipment!");
        }
    }
}