using System;
using System.Collections.Generic;
using Game.GamePlay.Upgrades;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

[Serializable]
public class UpgradeSystem
{
    private readonly IMoneyStorage _moneyStorage;
    [ShowInInspector] private readonly Dictionary<string, Upgrade> _upgrades = new();
    
    [Inject]
    public UpgradeSystem(IMoneyStorage moneyStorage, List<UpgradeConfig> upgradeConfigs, DiContainer diContainer)
    {
        _moneyStorage = moneyStorage;
        foreach (var upgradeConfig in upgradeConfigs)
        {
            Upgrade upgrade = upgradeConfig.CreateUpgrade();
            diContainer.Inject(upgrade);
            _upgrades.Add(upgrade.Id, upgrade);
        }
    }

    public bool TryUpgrade(string upgradeName)
    {
        if (!_upgrades.TryGetValue(upgradeName, out var upgrade))
        {
            Debug.LogWarning($"No upgrade with name {upgradeName} found!");
            return false;
        }

        if (!CanUpgrade(upgrade, out int cost))
        {
            return false;
        }
        _moneyStorage.SpendMoney(cost);
        upgrade.LevelUp();
        return true;
    }

    private bool CanUpgrade(Upgrade upgrade, out int cost)
    {
        cost = default;
        if (upgrade.IsMaxLevelUp)
        {
            Debug.LogWarning($"{upgrade} is already maxed!");
            return false;
        }
        
        var upgradesNextPrice = upgrade.NextPrice;
        if (!_moneyStorage.CanSpendMoney(upgradesNextPrice))
        {
            Debug.LogWarning($"Not enough money to level up {upgrade}! The price is {upgradesNextPrice}!");
            return false;
        }
        cost = upgradesNextPrice;
        return true;
    }
}