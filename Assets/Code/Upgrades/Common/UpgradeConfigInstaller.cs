using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "UpgradeConfigInstaller", menuName = "Installers/UpgradeConfigInstaller")]
public class UpgradeConfigInstaller : ScriptableObjectInstaller<UpgradeConfigInstaller>
{
    [SerializeField] public List<UpgradeConfig> _upgradeConfigs;
    public override void InstallBindings()
    {
        Container.Bind<List<UpgradeConfig>>().FromInstance(_upgradeConfigs).AsSingle().NonLazy();
    }
}