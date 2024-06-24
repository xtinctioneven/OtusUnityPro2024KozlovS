using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShootEmUp
{
    public class BulletSpawner :MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _poolContainer;
        [SerializeField] private int _initialPoolSize = 50;
        [SerializeField] private bool _isFlexible = true;

        private Pool _bulletPool;

        private void Awake()
        {
            _bulletPool = new Pool(_poolContainer, _bulletPrefab.gameObject, _initialPoolSize, _isFlexible);
        }

        public Bullet Spawn(Transform newParent, BulletManager.BulletData bulletData)
        {
            Bullet newBullet = _bulletPool.Spawn(newParent, bulletData.position).GetComponent<Bullet>();
            newBullet.SetColor(bulletData.color);
            newBullet.SetPhysicsLayer(bulletData.physicsLayer);
            newBullet.SetDamage(bulletData.damage);
            newBullet.SetTeam(bulletData.isPlayer);
            newBullet.SetVelocity(bulletData.velocity);
            newBullet.SetBulletManager(bulletData.bulletManager);
            return newBullet;
        }

        public void Unspawn(Bullet bullet)
        {
            _bulletPool.Unspawn(bullet.gameObject);
        }
    }
}

