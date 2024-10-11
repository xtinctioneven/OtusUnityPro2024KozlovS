using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public sealed class UnloadStorageCapacityUpgradeTable
{
    public float Step => _unloadStorageCapacityStep;

    [Space]
    [InfoBox("Unload Storage Capacity: Linear Function")]
    [SerializeField]
    private int _startUnloadStorageCapacity;

    [SerializeField]
    private int _endUnloadStorageCapacity;

    [ReadOnly]
    [SerializeField]
    private int _unloadStorageCapacityStep;

    [Space]
    [ListDrawerSettings(
        IsReadOnly = true,
        OnBeginListElementGUI = "DrawLabelForListElement"
    )]
    [SerializeField]
    private int[] _table;

    public int GetUnloadStorageCapacity(int level)
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
        _table[0] = _startUnloadStorageCapacity;
        _table[maxLevel - 1] = _endUnloadStorageCapacity;

        float loadStorageCapacityStep = (float)(_endUnloadStorageCapacity - _startUnloadStorageCapacity) / (maxLevel - 1);
        _unloadStorageCapacityStep = (int)Math.Floor(loadStorageCapacityStep);

        for (var i = 1; i < maxLevel - 1; i++)
        {
            var loadStorageCapacity = _startUnloadStorageCapacity + _unloadStorageCapacityStep * i;
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