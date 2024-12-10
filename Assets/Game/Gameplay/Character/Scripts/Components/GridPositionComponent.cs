using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class GridPositionComponent
    {
        public Vector2 Value { get; private set; }
        public Vector3 WorldGridPosition { get; private set; }

        public GridPositionComponent(Vector2 position)
        {
            Value = position;
        }

        public void SetGridPosition(Vector2 position)
        {
            Value = position;
        }

        public void SetWorldGridPosition(Vector3 position)
        {
            WorldGridPosition = position;
        }
    }
}