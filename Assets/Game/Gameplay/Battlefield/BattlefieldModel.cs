using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay
{
    public class BattlefieldModel
    {
        private TeamGridModel _leftGridModel;
        private TeamGridModel _rightGridModel;

        public BattlefieldModel(TeamGridData[] leftTeamGridData = null, TeamGridData[] rightTeamGridData = null)
        {
            _leftGridModel = new TeamGridModel(Team.Left, leftTeamGridData);
            _rightGridModel = new TeamGridModel(Team.Right, rightTeamGridData);
        }

        public List<IEntity> GetAllEntities()
        {
            List<IEntity> entities = new();
            entities.AddRange(GetLeftTeamEntities());
            entities.AddRange(GetRightTeamEntities());
            return entities;
        }

        public List<IEntity> GetRightTeamEntities()
        {
            return _rightGridModel.GetAllEntities().ToList();
        }

        public List<IEntity> GetLeftTeamEntities()
        {
            return _leftGridModel.GetAllEntities().ToList();
        }

        public List<IEntity> GetTargets(Team activeTeam, Vector2 sourcePosition, TargetType targetType = TargetType.SingleEnemy)
        {
            List<IEntity> targetEntities = new();
            switch (targetType)
            {
                case (TargetType.SingleEnemy):
                {
                    int sourceRow = (int)sourcePosition.y;
                    TeamGridModel targetTeam = activeTeam == Team.Left ? _rightGridModel : _leftGridModel;
                    //Get first entity in this row
                    IEntity targetEntity = GetFrontEntity(targetTeam, sourceRow);
                    targetEntities.Add(targetEntity);
                    //Get first entity in 1st row
                    //Get first entity in 3rd row
                    
                    //var possibleTargets = targetTeam == Team.Left ? GetLeftTeamEntities() : GetRightTeamEntities();
                    
                    break;
                }
                default:
                {
                    Debug.LogError($"Invalid target type: {targetType}");
                    break;
                }
            }
            return targetEntities;
        }

        private IEntity GetFrontEntity(TeamGridModel targetTeam, int sourceRow)
        {
            IEntity targetEntity = null;
            int checkedRowCount = 0;
            int nextRow = sourceRow;
            while (targetEntity == null && checkedRowCount < 3)
            {
                if (!targetTeam.TryGetFrontEntityInRow(nextRow, out targetEntity))
                {
                    if (nextRow == sourceRow)
                    {
                        if (sourceRow == 0)
                        {
                            nextRow++;
                        }
                        else
                        {
                            nextRow--;
                        }
                    }
                    else
                    {
                        if (nextRow == 1)
                        {
                            nextRow--;
                        }
                        else
                        {
                            nextRow = 2;
                        }
                    }
                    checkedRowCount++;
                }
            }

            if (targetEntity == null)
            {
                Debug.LogError("Target entity not found!");
            }
            return targetEntity;
        }

        public void Print()
        {
            Debug.Log("Left team:");
            _leftGridModel.Print();
            Debug.Log("Right team:");
            _rightGridModel.Print();
        }
    }
}