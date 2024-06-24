using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionComponent : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _bullet.BulletCollision();
        if (!collision.gameObject.TryGetComponent(out TeamComponent team))
        {
            return;
        }

        if (_bullet.IsPlayer == team.IsPlayer)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
        {
            hitPoints.TakeDamage(_bullet.Damage);
        }
    }
}
