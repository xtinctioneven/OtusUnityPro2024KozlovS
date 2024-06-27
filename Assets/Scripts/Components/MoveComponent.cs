using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float speed = 5.0f;
        [SerializeField] LevelBounds _levelBounds;
        
        public void MoveInDirection(Vector2 vector)
        {
            var nextPosition = this._rigidbody2D.position + vector * this.speed;
            if (!_levelBounds.InBounds(nextPosition))
            {
                return;
            }
            _rigidbody2D.MovePosition(nextPosition);
        }

        public void SetLevelBounds(LevelBounds levelBounds)
        {
            _levelBounds = levelBounds;
        }
    }
}