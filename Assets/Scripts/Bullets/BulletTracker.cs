using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletTracker : IGameFixedUpdateListener, IInitializable
    {
        private LevelBounds _levelBounds;
        private BulletManager _bulletManager;
        private readonly List<Bullet> _trackedBullets = new();

        [Inject]
        private void Construct(LevelBounds levelBounds, BulletManager bulletManager)
        {
            _levelBounds = levelBounds;
            _bulletManager = bulletManager;
        }

        public void Initialize()
        {
            IGameListener.Register(this);
        }

        public void OnGameFixedUpdate(float fixedDeltaTime)
        {
            for (int i = 0, count = _trackedBullets.Count; i < count; i++)
            {
                Bullet bullet = _trackedBullets[i];
                if (!_levelBounds.InBounds(bullet.Position))
                {
                    _bulletManager.RemoveBullet(bullet);
                    count--;
                }
            }
        }

        public void Track(Bullet bullet)
        {
            _trackedBullets.Add(bullet);
        }

        public void Untrack(Bullet bullet)
        {
            _trackedBullets.Remove(bullet);
        }
    }
}
