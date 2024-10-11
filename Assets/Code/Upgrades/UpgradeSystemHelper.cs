using System.Collections.Generic;
using Entities;
using Game.GamePlay.Conveyor;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class UpgradeSystemHelper : MonoBehaviour
{
    private const string LOAD_STORAGE_CAPACITY_UPGRADE_ID = "LoadStorageCapacity";
    private const string UNLOAD_STORAGE_CAPACITY_UPGRADE_ID = "UnloadStorageCapacity";
    private const string PRODUCE_TIME_UPGRADE_ID = "ProduceTime";
    [SerializeField] private UpgradeSystem _upgradeSystem;
    
    [Inject]
    public void Construct(UpgradeSystem upgradeSystem)
    {
        _upgradeSystem = upgradeSystem;
    }

    [Button]
    public void LevelUpLoadCapacity()
    {
        _upgradeSystem.TryUpgrade(LOAD_STORAGE_CAPACITY_UPGRADE_ID);
    }
    [Button]
    public void LevelUpUnloadCapacity()
    {
        _upgradeSystem.TryUpgrade(UNLOAD_STORAGE_CAPACITY_UPGRADE_ID);
    }
    [Button]
    public void LevelUpProduceTime()
    {
        _upgradeSystem.TryUpgrade(PRODUCE_TIME_UPGRADE_ID);
    }
}