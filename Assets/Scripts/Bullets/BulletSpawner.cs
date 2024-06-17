using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShootEmUp
{
    [Serializable]
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

        public Bullet Spawn(Transform newParent, BulletSystem.Args args)
        {
            Bullet newBullet = _bulletPool.Spawn(newParent, args.position).GetComponent<Bullet>();
            newBullet.SetColor(args.color);
            newBullet.SetPhysicsLayer(args.physicsLayer);
            newBullet.SetDamage(args.damage);
            newBullet.SetTeam(args.isPlayer);
            newBullet.SetVelocity(args.velocity);
            return newBullet;
        }

        public void Unspawn(Bullet bullet)
        {
            _bulletPool.Unspawn(bullet.gameObject);
        }
    }
}

