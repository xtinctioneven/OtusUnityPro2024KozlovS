using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class HealthComponent
    {
        public const int MIN_LIFE = 0;
        public event Action<int> OnLifeChanged;
        public event Action<int> OnMaxLifeChanged;
        public int MaxValue => _healthStat.CurrentMaxValue;
        public int HealthShortage => MaxValue - Value; 

        public int Value
        {
            get => _healthStat.Value;
            set => _healthStat.SetValue(value);
        }

        private Stat _healthStat;

        public HealthComponent(Stat healthStat)
        {

            if (healthStat.StatType != StatType.Health)
            {
                Debug.LogError("Error! Passed stat is not of type Health.!");
                return;
            }

            _healthStat = healthStat;
            _healthStat.OnValueChanged += OnValueChanged;
            _healthStat.OnMaxValueChanged += OnMaxValueChanged;
        }

        private void OnValueChanged(int newValue)
        {
            OnLifeChanged?.Invoke(newValue);
        }

        private void OnMaxValueChanged(int newMaxValue)
        {
            OnMaxLifeChanged?.Invoke(newMaxValue);
        }
    }
}