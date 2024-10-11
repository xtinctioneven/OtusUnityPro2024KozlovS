using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public sealed class LoadStorageCapacityUpgradeTable
{
    public float Step => _loadStorageCapacityStep;

    [Space]
    [InfoBox("Load Storage Capacity: Linear Function")]
    [SerializeField]
    private int _startLoadStorageCapacity;

    [SerializeField]
    private int _endLoadStorageCapacity;

    [ReadOnly]
    [SerializeField]
    private int _loadStorageCapacityStep;

    [Space]
    [ListDrawerSettings(
        IsReadOnly = true,
        OnBeginListElementGUI = "DrawLabelForListElement"
    )]
    [SerializeField]
    private int[] _table;

    public int GetLoadStorageCapacity(int level)
    {
        var index = level - 1;
        return _table[index];
    }

    public void OnValidate(int maxLevel)
    {
        EvaluateTable(maxLevel);
    }

    private void EvaluateTable(int maxLevel)
    {
        _table = new int[maxLevel];
        _table[0] = _startLoadStorageCapacity;
        _table[maxLevel - 1] = _endLoadStorageCapacity;

        float loadStorageCapacityStep = (float)(_endLoadStorageCapacity - _startLoadStorageCapacity) / (maxLevel - 1);
        _loadStorageCapacityStep = (int)Math.Floor(loadStorageCapacityStep);

        for (var i = 1; i < maxLevel - 1; i++)
        {
            var loadStorageCapacity = _startLoadStorageCapacity + _loadStorageCapacityStep * i;
            _table[i] = loadStorageCapacity;
        }
    }
    
#if UNITY_EDITOR
    private void DrawLabelForListElement(int index)
    {
        GUILayout.Space(8);
        GUILayout.Label($"Level {index + 1}");
    }
#endif
}