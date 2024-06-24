using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private BulletTracker _bulletTracker;
        
        public void Shoot(GameObject shooter, Vector2 direction)
        {
            BulletConfig bulletConfig = shooter.GetComponent<WeaponComponent>().GetBulletConfig();
            WeaponComponent weaponComponent = shooter.GetComponent<WeaponComponent>();
            TeamComponent teamComponent = shooter.GetComponent<TeamComponent>();
            BulletData bulletData = new BulletData
            {
                isPlayer = teamComponent.IsPlayer,
                physicsLayer = (int)bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = weaponComponent.Position,
                velocity = direction * bulletConfig.speed,
                bulletManager = this
            };
            Bullet newBullet = _bulletSpawner.Spawn(_worldTransform, bulletData);
            _bulletTracker.Track(newBullet);
        }

        public void RemoveBullet(Bullet bullet)
        {
            _bulletSpawner.Unspawn(bullet);
            _bulletTracker.Untrack(bullet);
        }

        public void BulletCollisionCallback(Bullet bullet)
        {
            RemoveBullet(bullet);
        }

        public struct BulletData
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
            public BulletManager bulletManager;
        }
    }
}