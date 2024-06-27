using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, IGameFixedUpdateListener, IGamePauseListener, IGameFinishListener, IGamePlayListener
    {
        [SerializeField] private LevelBackgroundParams _backgroudnParams;
        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;
        private Transform _selfTransform;

        private void Awake()
        {
            _startPositionY = _backgroudnParams._startPositionY;
            _endPositionY = _backgroudnParams._endPositionY;
            _movingSpeedY = _backgroudnParams._movingSpeedY;
            _selfTransform = transform;
            var position = _selfTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        private void Start()
        {
            IGameListener.Register(this);
        }

        public void OnGamePause()
        {
            enabled = false;
        }

        public void OnGamePlay()
        {
            enabled = true;
        }

        public void OnGameFinish()
        {
            enabled = false;
        }

        public void OnGameFixedUpdate(float fixedDeltaTime)
        {
            if (_selfTransform.position.y <= _endPositionY)
            {
                _selfTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _selfTransform.position -= new Vector3(
                _positionX,
                _movingSpeedY * fixedDeltaTime,
                _positionZ
            );
        }

        [Serializable]
        public sealed class LevelBackgroundParams
        {
            [SerializeField]public float _startPositionY;
            [SerializeField] public float _endPositionY;
            [SerializeField] public float _movingSpeedY;
        }
    }
}