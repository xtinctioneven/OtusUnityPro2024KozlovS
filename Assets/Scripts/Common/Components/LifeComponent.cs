using System;
using UnityEngine;

[Serializable]
public class LifeComponent
{
    public const int MIN_LIFE = -100;
    public event Action<int> OnValueChanged;

    public int MaxValue => _maxHitPoints;

    public int Value
    {
        get => _currentHitPoints;
        set
        {
            _currentHitPoints = value;
            OnValueChanged?.Invoke(_currentHitPoints);
        } 
    }
    private int _maxHitPoints;
    private int _currentHitPoints;
    
    public LifeComponent(int maxHitPoints)
    {
        _maxHitPoints = maxHitPoints;
        _currentHitPoints = maxHitPoints;
    }
}