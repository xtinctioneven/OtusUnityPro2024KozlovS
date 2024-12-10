using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class TeamGridModel
    {
        public Team Team { get; private set; }
        private GridCell[,] _teamGrid;

        public TeamGridModel(Team team)
        {
            _teamGrid = new GridCell[3, 3];
            Team = team;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector2 gridPosition = new Vector2(j, i);
                    _teamGrid[i, j] = new GridCell(team, gridPosition);
                }
            }
        }

        public void SetupEntity(IEntity entity, Vector2 gridPosition)
        {
            _teamGrid[(int)gridPosition.x, (int)gridPosition.y].SetEntity(entity);
            entity.GetEntityComponent<TeamComponent>().SetTeam(Team);
            entity.GetEntityComponent<GridPositionComponent>().SetGridPosition(gridPosition);
        }

        public List<IEntity> GetAllEntities()
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
            return entities;
        }

        public List<Vector2> GetOccupiedPositions()
        {
            List<Vector2> positions = new List<Vector2>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_teamGrid[i, j].Entity != null)
                    {
                        positions.Add(_teamGrid[i, j].Position);
                    }
                }
            }
            return positions;
        }

        public List<GridCell> GetOccupiedCells()
        {
            List<GridCell> cells = new List<GridCell>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_teamGrid[i, j].Entity != null)
                    {
                        cells.Add(_teamGrid[i, j]);
                    }
                }
            }
            return cells;
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
                        Debug.Log($"Position [{gridPosition.x}, {gridPosition.y}]. Cell is occupied by {_teamGrid[i, j].Entity.Name}");
                    }
                    else
                    {
                        Debug.Log($"Position [{gridPosition.x}, {gridPosition.y}]. Cell is empty");
                    }
                }
            }
        }
        
        // private void ApplyGridData(TeamGridData gridData)
        // {
        //     gridData.Entity.GetEntityComponent<TeamComponent>().SetTeam(Team);
        //     gridData.Entity.GetEntityComponent<GridPositionComponent>().SetPosition(gridData.Position);
        // }
    }
}