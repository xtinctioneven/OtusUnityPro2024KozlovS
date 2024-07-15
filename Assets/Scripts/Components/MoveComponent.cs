using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public sealed class MoveComponent: UnitComponent
    {
        private float _speed = 5.0f;
        private Rigidbody2D _rigidbody2D;
        private LevelBounds _levelBounds;

        [Inject]
        public void Construct (Rigidbody2D rigidbody2D, LevelBounds levelBounds, float speed)
        {
            _rigidbody2D = rigidbody2D;
            _levelBounds = levelBounds;
            _speed = speed;
        }
        
        public void MoveInDirection(Vector2 vector)
        {
            var nextPosition = _rigidbody2D.position + vector * _speed;
            if (!_levelBounds.InBounds(nextPosition))
            {
                return;
            }
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}