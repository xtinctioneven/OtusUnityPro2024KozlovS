public class EquipmentEffectObserver
{
    private readonly Equipment _equipment;
    private readonly Hero _hero;
    
    public EquipmentEffectObserver(Equipment equipment, Hero hero)
    {
        _equipment = equipment;
        _hero = hero;
    }
    
    public void Construct()
    {
        _equipment.OnEquipped += OnItemEquipped;
        _equipment.OnUnequipped += OnItemUnequipped;
    }
    
    private void OnItemEquipped(InventoryItem inventoryItem)
    {
        var components = inventoryItem.GetComponents();
        foreach (var component in components)
        {
            if (component is IEquippableComponent equippableComponent)
            {
                equippableComponent.Apply(_hero);
            }
        }
    }
    
    private void OnItemUnequipped(InventoryItem inventoryItem)
    {
        var components = inventoryItem.GetComponents();
        foreach (var component in components)
        {
            if (component is IEquippableComponent equippableComponent)
            {
                equippableComponent.Discard(_hero);
            }
        }
    }
}