using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour, IInitializable, IGamePauseListener, IGamePlayListener, IGameFinishListener
    {
        public int Damage { get { return _damage; } }
        public bool IsPlayer { get { return _isPlayer; } }
        public Vector3 Position { get { return this.transform.position; } }

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private BulletManager _bulletManager;
        private bool _isPlayer;
        private int _damage;
        private Vector2 _cachedVelocity;

        [Inject]
        private void Construct(Rigidbody2D rigidbody2D, SpriteRenderer spriteRenderer, BulletManager bulletManager)
        {
            _rigidbody2D = rigidbody2D;
            _spriteRenderer = spriteRenderer;
            _bulletManager = bulletManager;
        }

        public void Initialize()
        {
            IGameListener.Register(this);
        }

        public void OnGamePause()
        {
            _cachedVelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void OnGamePlay()
        {
            _rigidbody2D.velocity = _cachedVelocity;
        }

        public void OnGameFinish()
        {
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void Setup(Vector2 velocity, int physicsLayer, Color color, bool isPlayer, int damage)
        {
            _rigidbody2D.velocity = velocity;
            this.gameObject.layer = physicsLayer;
            _spriteRenderer.color = color;
            _isPlayer = isPlayer;
            _damage = damage;
        }

        public void BulletCollision()
        {
            _bulletManager.BulletCollisionCallback(this);
        }
    }
}