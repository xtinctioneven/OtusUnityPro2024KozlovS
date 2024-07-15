using UnityEngine;
using Zenject;


namespace ShootEmUp
{
    public class BulletSpawner
    {
        private Pool _bulletPool;

        [Inject]
        private void Construct([Inject(Id = SceneInstaller.BULLET_POOL)]Pool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public Bullet Spawn(Transform newParent, BulletManager.BulletData bulletData)
        {
            Bullet newBullet = _bulletPool.Spawn(newParent, bulletData.position).GetComponent<Bullet>();
            newBullet.Setup(
                bulletData.velocity, 
                bulletData.physicsLayer, 
                bulletData.color, 
                bulletData.isPlayer, 
                bulletData.damage
                );
            return newBullet;
        }

        public void Unspawn(Bullet bullet)
        {
            _bulletPool.Unspawn(bullet.gameObject);
        }
    }
}

