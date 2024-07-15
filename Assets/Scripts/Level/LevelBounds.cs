using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class LevelBounds
    {
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private Transform _downBorder;
        [SerializeField] private Transform _topBorder;
        
        [Inject]        
        private void Construct(LevelBoundsSettings settings)
        {
            _leftBorder = settings.LeftBorder;
            _rightBorder = settings.RightBorder;
            _downBorder = settings.DownBorder;
            _topBorder = settings.TopBorder;
        }        

        public bool InBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > this._leftBorder.position.x
                   && positionX < this._rightBorder.position.x
                   && positionY > this._downBorder.position.y
                   && positionY < this._topBorder.position.y;
        }
    }

    [Serializable]
    public struct LevelBoundsSettings
    {
        public Transform LeftBorder;
        public Transform RightBorder;
        public Transform DownBorder;
        public Transform TopBorder;
    }
}