using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class TeamGridModel
    {
        public Team Team { get; private set; }
        private GridCell[,] _teamGrid;

        public TeamGridModel(Team team, TeamGridData[] teamGridData = null)
        {
            _teamGrid = new GridCell[3, 3];
            Team = team;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector2 gridPosition = new Vector2(j, i);
                    GridCell gridCell = null;
                    foreach (var gridData in teamGridData)
                    {
                        if (gridData.Position == gridPosition)
                        {
                            gridCell = new GridCell(team, gridPosition, gridData.Entity);
                            ApplyGridData(gridData);
                        }
                    }

                    if (gridCell == null)
                    {
                        gridCell = new GridCell(team, gridPosition);
                    }
                    _teamGrid[i, j] = gridCell;
                }
            }
        }

        public IEntity[] GetAllEntities()
        {
            List<IEntity> entities = new List<IEntity>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_teamGrid[i, j].Entity != null)
                    {
                        entities.Add(_teamGrid[i, j].Entity);
                    }
                }
            }
            return entities.ToArray();
        }

        public bool TryGetFrontEntityInRow(int rowIndex, out IEntity entity)
        {
            entity = null;
            for (int i = 0; i < 3; i++)
            {
                if (_teamGrid[i, rowIndex].Entity != null)
                {
                    entity = _teamGrid[i, rowIndex].Entity;
                    return true;
                }
            }
            return false;
        }
        
        public void Print()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector2 gridPosition = new Vector2(j, i);
                    if (_teamGrid[i, j].Entity != null)
                    {
                        Debug.Log($"Position [{gridPosition.x}, {gridPosition.y}]. Cell is occupied by {_teamGrid[i, j].Entity}");
                    }
                    else
                    {
                        Debug.Log($"Position [{gridPosition.x}, {gridPosition.y}]. Cell is empty");
                    }
                }
            }
        }
        
        private void ApplyGridData(TeamGridData gridData)
        {
            gridData.Entity.GetEntityComponent<TeamComponent>().SetTeam(Team);
            gridData.Entity.GetEntityComponent<PositionComponent>().SetPosition(gridData.Position);
        }
    }
}