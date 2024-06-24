using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnHpEmpty;
        
        [SerializeField] private int _maxHitPoints;
        private int _currentHitPoints;

        private void Awake()
        {
            _currentHitPoints = _maxHitPoints;
        }

        public bool IsHitPointsExists() {
            return _currentHitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _currentHitPoints -= damage;
            if (_currentHitPoints <= 0)
            {
                OnHpEmpty?.Invoke(this.gameObject);
            }
        }

        public void Reset()
        {
            _currentHitPoints = _maxHitPoints;
        }
    }
}