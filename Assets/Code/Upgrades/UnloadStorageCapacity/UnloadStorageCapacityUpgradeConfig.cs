using UnityEngine;

[CreateAssetMenu(
    fileName = "UnloadStorageCapacityUpgradeConfig",
    menuName = "UpgradeConfigs/New UnloadStorageCapacityUpgradeConfig"
)]
public class UnloadStorageCapacityUpgradeConfig : UpgradeConfig
{
    public UnloadStorageCapacityUpgradeTable UpgradeTable;

    protected override void OnValidate()
    {
        base.OnValidate();
        UpgradeTable.OnValidate(MaxLevel);
    }

    public override Upgrade CreateUpgrade()
    {
        return new UnloadStorageCapacityUpgrade(this);
    }
}