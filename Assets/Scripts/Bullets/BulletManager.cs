using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletManager
    {
        private Transform _worldTransform;
        private BulletSpawner _bulletSpawner;
        private BulletTracker _bulletTracker;

        [Inject]
        private void Construct(
            [Inject(Id = SceneInstaller.WORLD_TRANSFORM)] Transform worldTransform, 
            BulletSpawner bulletSpawner, 
            BulletTracker bulletTracker
            )
        {
            _worldTransform = worldTransform;
            _bulletSpawner = bulletSpawner;
            _bulletTracker = bulletTracker;
        }

        public void Shoot(Unit shooter, Vector2 direction)
        {
            WeaponComponent weaponComponent = shooter.WeaponComponent;
            BulletConfig bulletConfig = weaponComponent.GetBulletConfig();
            TeamComponent teamComponent = shooter.TeamComponent;
            BulletData bulletData = new BulletData
            {
                isPlayer = teamComponent.IsPlayer,
                physicsLayer = (int)bulletConfig.PhysicsLayer,
                color = bulletConfig.Color,
                damage = bulletConfig.Damage,
                position = weaponComponent.Position,
                velocity = direction * bulletConfig.Speed,
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