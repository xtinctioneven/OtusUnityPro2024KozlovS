using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemConfig", menuName = "Inventory/New InventoryItemConfig")]
public class InventoryItemConfig : ScriptableObject
{
    public InventoryItem Item;
}