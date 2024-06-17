using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private BulletSpawner _bulletSpawner;
        private readonly List<Bullet> _activeBullets = new();
        
        private void FixedUpdate()
        {
            for (int i = 0, count = _activeBullets.Count; i < count; i++)
            {
                var bullet = _activeBullets[i];
                if (!_levelBounds.InBounds(bullet.Position))
                {
                    RemoveBullet(bullet);
                    count--;
                }
            }
        }

        public void Shoot(GameObject shooter, Vector2? direction = null)
        {
            BulletConfig bulletConfig = shooter.GetComponent<WeaponComponent>().GetBulletConfig();
            WeaponComponent weaponComponent = shooter.GetComponent<WeaponComponent>();
            TeamComponent teamComponent = shooter.GetComponent<TeamComponent>();
            if (direction != null)
            {
                Vector2 velocity = direction.Value;
            }
            Args bulletArgs = new Args
            {
                isPlayer = teamComponent.IsPlayer,
                physicsLayer = (int)bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = weaponComponent.Position,
                velocity = direction == null ? 
                    Vector3.up * bulletConfig.speed : direction.Value * bulletConfig.speed
            };
            Bullet newBullet = _bulletSpawner.Spawn(_worldTransform, bulletArgs);
            newBullet.OnCollisionEntered += Bullet_OnCollisionEntered;
            _activeBullets.Add(newBullet);
        }

        private void Bullet_OnCollisionEntered(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            this.RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            _bulletSpawner.Unspawn(bullet);
            bullet.OnCollisionEntered -= this.Bullet_OnCollisionEntered;
            _activeBullets.Remove(bullet);
        }
        
        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
}