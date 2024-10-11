using UnityEngine;

[CreateAssetMenu(
    fileName = "ProduceTimeUpgradeConfig",
    menuName = "UpgradeConfigs/New ProduceTimeUpgradeConfig"
)]
public class ProduceTimeUpgradeConfig : UpgradeConfig
{
    public ProduceTimeUpgradeTable UpgradeTable;
    
     protected override void OnValidate()
     {
         base.OnValidate();
         UpgradeTable.OnValidate(MaxLevel);
     }

    public override Upgrade CreateUpgrade()
    {
        return new ProduceTimeUpgrade(this);
    }
}