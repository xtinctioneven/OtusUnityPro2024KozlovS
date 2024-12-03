using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class GridPositionComponent
    {
        public Vector2 Value { get; private set; }

        public GridPositionComponent(Vector2 position)
        {
            Value = position;
        }

        public void SetPosition(Vector2 position)
        {
            Value = position;
        }
    }
}