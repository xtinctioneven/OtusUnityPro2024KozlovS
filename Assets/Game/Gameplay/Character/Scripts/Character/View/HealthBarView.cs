using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Slider))]
    public class HealthBarView : MonoBehaviour, IHealthView
    {
        [SerializeField] private float _valueUpdateDuration = 0.5f;
        private Slider _healthSlider;
        private HealthComponent _healthComponent;
        private int _health;
        private int _healthMax;

        private void Awake()
        {
            _healthSlider = GetComponent<Slider>();
        }

        public void Setup(HealthComponent healthComponent)
        {
            Enable();
            _healthComponent = healthComponent;
            _health = _healthComponent.Value;
            _healthMax = _healthComponent.MaxValue;
            UpdateView();
        }

        public void UpdateCurrentHealth(int newHealth)
        {
            _health = newHealth;
            UpdateView();
        }

        public void UpdateMaxHealth(int newMaxHealth)
        {
            _healthMax = newMaxHealth;
            UpdateView();
        }

        public void UpdateView()
        {
            float newValue = _health / (float)_healthMax;
            _healthSlider.DOValue(newValue, _valueUpdateDuration).SetEase(Ease.InOutQuad);
        }

        public void Disable()
        {
            this.gameObject.SetActive(false);
        }

        public void Enable()
        {
            this.gameObject.SetActive(true);
        }
    }
}