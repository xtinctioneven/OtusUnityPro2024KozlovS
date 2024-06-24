using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTracker : MonoBehaviour
{
    [SerializeField] private LevelBounds _levelBounds;
    [SerializeField] private BulletManager _bulletManager;
    private readonly List<Bullet> _trackedBullets = new();
    
    private void FixedUpdate()
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
