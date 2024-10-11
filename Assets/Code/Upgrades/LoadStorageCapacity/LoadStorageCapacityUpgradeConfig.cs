using UnityEngine;

[CreateAssetMenu(
    fileName = "LoadStorageCapacityUpgradeConfig",
    menuName = "UpgradeConfigs/New LoadStorageCapacityUpgradeConfig"
)]
public class LoadStorageCapacityUpgradeConfig : UpgradeConfig
{
    public LoadStorageCapacityUpgradeTable UpgradeTable;

    protected override void OnValidate()
    {
        base.OnValidate();
        UpgradeTable.OnValidate(MaxLevel);
    }

    public override Upgrade CreateUpgrade()
    {
        return new LoadStorageCapacityUpgrade(this);
    }
}