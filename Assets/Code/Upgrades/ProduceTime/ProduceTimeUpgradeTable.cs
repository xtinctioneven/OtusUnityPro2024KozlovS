using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public sealed class ProduceTimeUpgradeTable
{
    public float Step => _produceTimeStep;

    [Space]
    [InfoBox("Produce Time: Geometric Function")]
    [SerializeField]
    private float _startProduceTime;

    [SerializeField]
    private float _produceTimeStep;

    [Space]
    [ListDrawerSettings(
        IsReadOnly = true,
        OnBeginListElementGUI = "DrawLabelForListElement"
    )]
    [SerializeField]
    private float[] _table;

    public float GetProduceTime(int level)
    {
        var index = level - 1;
        return _table[index];
    }

    public void OnValidate(int maxLevel)
    {
        this.EvaluateTable(maxLevel);
    }

    private void EvaluateTable(int maxLevel)
    {
        _table = new float[maxLevel];
        _table[0] = _startProduceTime;
        for (var i = 1; i < maxLevel; i++)
        {
            _table[i] = (float)Math.Round(_table[i - 1] * _produceTimeStep, 2);
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