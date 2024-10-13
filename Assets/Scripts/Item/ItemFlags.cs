using System;

[Flags]
public enum ItemFlags
{
    None = 0,
    Consumable = 1,
    Stackable = 2,
    Effectible = 4,
    Equippable = 8,
}