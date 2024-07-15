using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace ShootEmUp
{
    public sealed class LevelBackground : IInitializable, IGameFixedUpdateListener
    {
        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;
        private Transform _transform;

        [Inject]
        private void Construct(LevelBackgroundSettings levelBackgroundSettings)
        {
            _startPositionY = levelBackgroundSettings.SartPositionY;
            _endPositionY = levelBackgroundSettings.EndPositionY;
            _movingSpeedY = levelBackgroundSettings.MovingSpeedY;
            _transform = levelBackgroundSettings.BackgroundTransform;
            Vector2 position = _transform.position;
            _positionX = position.x;
            _positionX = position.y;
        }

        public void Initialize()
        {
            IGameListener.Register(this);
        }

        public void OnGameFixedUpdate(float fixedDeltaTime)
        {
            if (_transform.position.y <= _endPositionY)
            {
                _transform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _transform.position -= new Vector3(
                _positionX,
                _movingSpeedY * fixedDeltaTime,
                _positionZ
            );
        }
    }

    [Serializable]
    public sealed class LevelBackgroundSettings
    {
        public float SartPositionY;
        public float EndPositionY;
        public float MovingSpeedY;
        public Transform BackgroundTransform;
    }
}