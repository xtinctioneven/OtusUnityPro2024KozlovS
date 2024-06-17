using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
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

        public event Action<Bullet, Collision2D> OnCollisionEntered;

        private bool _isPlayer;
        private int _damage;

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

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
    }
}