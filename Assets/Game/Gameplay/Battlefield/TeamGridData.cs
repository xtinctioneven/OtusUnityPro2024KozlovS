using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class TeamGridData
    {
        public Vector2 Position;
        [field: SerializeReference] public IEntity Entity;
    }
}