using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public sealed class GeometricUpgradePriceTable : IPriceTable
{
    [Space]
    [SerializeField]
    private int _basePrice;
    
    [Space]
    [SerializeField]
    private float _multiplier = 1.33f;
    
    [Space]
    [ListDrawerSettings(OnBeginListElementGUI = "DrawLevels")]
    [SerializeField]
    private int[] _levels;

    public int GetPrice(int level)
    {
        var index = level - 1;
        index = Mathf.Clamp(index, 0, _levels.Length - 1);
        return _levels[index];
    }

    private void DrawLevels(int index)
    {
        GUILayout.Space(8);
        GUILayout.Label($"Level #{index + 1}");
    }

    public void OnValidate(int maxLevel)
    {
        EvaluatePriceTable(maxLevel);
    }

    private void EvaluatePriceTable(int maxLevel)
    {
        var table = new int[maxLevel];
        table[0] = new int();
        table[1] = _basePrice;
        for (var level = 2; level < maxLevel; level++)
        {
            int price =  (int)Mathf.Floor(table[level-1]*_multiplier);
            table[level] = price;
        }
        _levels = table;
    }
}