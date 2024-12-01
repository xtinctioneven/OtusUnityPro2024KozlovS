using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class PositionComponent
    {
        public Vector2 Value { get; private set; }

        public PositionComponent(Vector2 position)
        {
            Value = position;
        }

        public void SetPosition(Vector2 position)
        {
            Value = position;
        }
    }
}