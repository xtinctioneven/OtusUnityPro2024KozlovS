using UnityEngine;

namespace Game.Gameplay
{
    public class GridCell
    {
        public IEntity Entity { get; private set; } = null;
        public Vector2 Position { get; private set; }
        public Team Team { get; private set; }

        public GridCell(Team team, Vector2 position, IEntity entity = null)
        {
            Team = team;
            Position = position;
            if (entity != null)
            {
                SetEntity(entity);
            }
        }
        
        public void SetEntity(IEntity entity)
        {
            Entity = entity;
        }
    }
}