using System;
using UnityEngine;

public abstract class UpgradeConfig : ScriptableObject
{
    public string Id;
    public int MaxLevel;
    [SerializeReference] public IPriceTable PriceTable;

    protected virtual void OnValidate()
    {
        PriceTable.OnValidate(MaxLevel);
    }

    public abstract Upgrade CreateUpgrade();
}