using System;
using System.Collections;
using System.Collections.Generic;
using Game.GamePlay.Upgrades;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class MoneyStorageHelper : MonoBehaviour
{  
    private MoneyStorage _moneyStorage;
    [ShowInInspector, ReadOnly] private int _currentMoney;

    [Inject]
    public void Construct(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
    }
    
    private void Start()
    {
        _currentMoney = _moneyStorage.Money;
        _moneyStorage.OnMoneyChanged += MoneyStorageOnMoneyChanged;
    }

    private void MoneyStorageOnMoneyChanged(int newMoneyValue)
    {
        _currentMoney = newMoneyValue;
    }

    [Button]
    public void AddMoney(int amountToAdd)
    {
        _moneyStorage.EarnMoney(amountToAdd);
    }

    [Button]
    public void RemoveMoney(int amountToRemove)
    {
        _moneyStorage.SpendMoney(amountToRemove);
    }
}
