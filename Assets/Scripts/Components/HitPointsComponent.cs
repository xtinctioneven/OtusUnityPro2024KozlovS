using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public sealed class HitPointsComponent : UnitComponent
    {
        public event Action<Unit> OnHpEmpty;
        
        private int _maxHitPoints;
        private int _currentHitPoints;

        [Inject]
        private void Construct(int maxHitPoints)
        {
            _maxHitPoints = maxHitPoints;
            _currentHitPoints = maxHitPoints;
        }

        public bool IsHitPointsExists() {
            return _currentHitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _currentHitPoints -= damage;
            if (_currentHitPoints <= 0)
            {
                OnHpEmpty?.Invoke(_componentOwner);
            }
        }

        public void Reset()
        {
            _currentHitPoints = _maxHitPoints;
        }
    }
}