using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public sealed class WeaponComponent : UnitComponent
    {
        public Vector2 Position { get { return this._firePoint.position; }
        }

        private Transform _firePoint;
        private BulletConfig _bulletConfig;

        public BulletConfig GetBulletConfig()
        {
            return _bulletConfig;
        }

        [Inject]
        public void Construct(BulletConfig bulletConfig, Transform firePoint)
        {
            _bulletConfig = bulletConfig;
            _firePoint = firePoint;
        }
    }
}