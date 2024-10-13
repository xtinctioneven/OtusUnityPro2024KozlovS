using UnityEngine;

public class InventoryEffectObserver
{
    private Inventory _inventory;
    private Hero _hero;
    
    public InventoryEffectObserver(Inventory inventory, Hero hero)
    {
        _inventory = inventory;
        _hero = hero;
    }
    
    public void Construct()
    {
        _inventory.OnAdded += OnItemAdded;
        _inventory.OnRemoved += OnItemRemoved;
        _inventory.OnConsumed += OnItemConsumed;
    }

    private void OnItemAdded(InventoryItem inventoryItem)
    {
        if ((inventoryItem.Flags & ItemFlags.Effectible) == ItemFlags.Effectible)
        {
            var components = inventoryItem.GetComponents();
            foreach (var component in components)
            {
                if (component is IEffectableComponent effectableComponent)
                {
                    effectableComponent.Apply(_hero);
                }
            }
        }
    }

    private void OnItemRemoved(InventoryItem inventoryItem)
    {
        if ((inventoryItem.Flags & ItemFlags.Effectible) == ItemFlags.Effectible)
        {
            var components = inventoryItem.GetComponents();
            foreach (var component in components)
            {
                if (component is IEffectableComponent effectableComponent)
                {
                    effectableComponent.Discard(_hero);
                }
            }
        }
    }

    private void OnItemConsumed(InventoryItem item)
    {
        var components = item.GetComponents();
        foreach (var component in components)
        {
            if (component is IConsumableComponent consumableComponent)
            {
                consumableComponent.Apply(_hero);
            }
        }
    }
}