using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletCollisionComponent : MonoBehaviour
    {
        private Bullet _bullet;
        [Inject]
        private void Constuct(Bullet bullet)
        {
            _bullet = bullet;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _bullet.BulletCollision();
            if (!collision.gameObject.TryGetComponent(out Unit unit))
            {
                return;
            }

            if (_bullet.IsPlayer == unit.TeamComponent.IsPlayer)
            {
                return;
            }

            unit.HitPointsComponent.TakeDamage(_bullet.Damage);
        }
    }
}
