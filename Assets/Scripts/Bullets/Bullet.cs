using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public int Damage
        {
            get { return _damage; }
        }
        public bool IsPlayer
        {
            get { return _isPlayer; }
        }
        public Vector3 Position
        {
            get { return this.transform.position; }
        }

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private BulletManager _bulletManager;
        private bool _isPlayer;
        private int _damage;

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            this.gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void SetTeam(bool isPlayer)
        {
            _isPlayer = isPlayer;
        }

        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        public void SetBulletManager(BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
        }

        public void BulletCollision()
        {
            _bulletManager.BulletCollisionCallback(this);
        }
    }
}