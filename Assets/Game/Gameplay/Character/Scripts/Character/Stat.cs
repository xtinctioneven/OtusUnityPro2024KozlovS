using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class Stat
    {
        public Action<int> OnValueChanged;
        public Action<int> OnMaxValueChanged;
        public StatType StatType { get; private set; }
        public int Value { get; private set; }
        public int BaseMaxValue { get; private set; }
        public int CurrentMaxValue { get; private set; }

        public Stat(StatType statType, int baseValue)
        {
            StatType = statType;
            Value = baseValue;
            BaseMaxValue = baseValue;
            CurrentMaxValue = baseValue;
        }

        public void SetValue(int newValue)
        {
            Value = Mathf.Clamp(newValue, 0, CurrentMaxValue);
            OnValueChanged?.Invoke(Value);
        }

        public void SetMaxValue(int newMaxValue)
        {
            int newValue = Value * (newMaxValue / CurrentMaxValue);
            CurrentMaxValue = newValue;
            OnMaxValueChanged?.Invoke(CurrentMaxValue);
            SetValue(newValue);
        }

        [Serializable]
        public struct StatData
        {
            public StatType StatType;
            public int Value;
        }
    }
}