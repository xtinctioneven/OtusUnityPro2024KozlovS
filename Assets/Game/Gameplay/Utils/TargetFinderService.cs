using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Gameplay
{
    public class TargetFinderService
    {
        private readonly (int, int)[] FIRST_ROW_SORT_ORDER = new (int, int)[9]
        {
            (0, 0), (1, 0), (2, 0),
            (0, 1), (1, 1), (2, 1),
            (0, 2), (1, 2), (2, 2)
        };
        private readonly (int, int)[] SECOND_ROW_SORT_ORDER = new (int, int)[9]
        {
            (0, 1), (1, 1), (2, 1),
            (0, 0), (1, 0), (2, 0),
            (0, 2), (1, 2), (2, 2)
        };
        private readonly (int, int)[] THIRD_ROW_SORT_ORDER = new (int, int)[9]
        {
            (0, 2), (1, 2), (2, 2),
            (0, 1), (1, 1), (2, 1),
            (0, 0), (1, 0), (2, 0)
        };
        
        private BattlefieldModel _battlefieldModel;
        private TeamGridModel _leftGridModel;
        private TeamGridModel _rightGridModel;
        private DiContainer _diContainer;
        
        [Inject]
        public TargetFinderService(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            _battlefieldModel = _diContainer.Resolve<BattlefieldModel>();
            _rightGridModel = _battlefieldModel.RightGridModel;
            _leftGridModel = _battlefieldModel.LeftGridModel;
        }
        
        public List<IEntity> GetTargets(IEntity entity, IEffectTarget ability)
        {
            List<IEntity> targetEntities = new();
            if (ability.TargetType == TargetType.None)
            {
                return targetEntities;
            }
            Vector2 sourcePosition = entity.GetEntityComponent<GridPositionComponent>().Value; 
            int targetsCount = ability.TargetsCount;
            TargetType targetType = ability.TargetType;
            switch (targetType)
            {
                case (TargetType.EnemyTarget):
                {
                    int sourceRow = (int)sourcePosition.y;
                    Team activeTeam = entity.GetEntityComponent<TeamComponent>().Value;
                    TeamGridModel targetTeamGrid = activeTeam == Team.Left ? _rightGridModel : _leftGridModel;
                    List<GridCell> possibleTargets = targetTeamGrid.GetOccupiedCells();
                    possibleTargets.RemoveAll(x => x.Entity.GetEntityComponent<DeathComponent>().IsDead);
                    SortTargetsByPriority(possibleTargets, ability.TargetPriority, sourcePosition);
                    for (int i = 0; i < targetsCount; i++)
                    {
                        if (i < possibleTargets.Count)
                        {
                            targetEntities.Add(possibleTargets[i].Entity);
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                }
                case (TargetType.EnemyRow):
                {
                    //TODO: extension
                    break;
                }
                case (TargetType.EnemyColumn):
                {
                    //TODO: extension
                    break;
                }
                case (TargetType.AllyTarget):
                {
                    //TODO
                    int sourceRow = (int)sourcePosition.y;
                    Team activeTeam = entity.GetEntityComponent<TeamComponent>().Value;
                    TeamGridModel targetTeamGrid = activeTeam == Team.Left ? _leftGridModel : _rightGridModel;
                    List<GridCell> possibleTargets = targetTeamGrid.GetOccupiedCells();
                    possibleTargets.RemoveAll(x => x.Entity.GetEntityComponent<DeathComponent>().IsDead);
                    SortTargetsByPriority(possibleTargets, ability.TargetPriority, sourcePosition);
                    for (int i = 0; i < targetsCount; i++)
                    {
                        if (i < possibleTargets.Count)
                        {
                            targetEntities.Add(possibleTargets[i].Entity);
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                    break;
                }
                case (TargetType.AllyRow):
                {
                    //TODO: extension
                    break;
                }
                case (TargetType.AllyColumn):
                {
                    //TODO: extension
                    break;
                }
                default:
                {
                    Debug.LogError($"Target type {targetType} is not supported!");
                    break;
                }
            }
            Debug.Log($"{targetEntities}");
            return targetEntities;
        }

        private void SortTargetsByPriority(List<GridCell> possibleTargets, TargetPriorityType targetPriority, Vector2 sourcePosition)
        {
            List<GridCell> sortedTargets = new List<GridCell>();
            (int,int)[] sortOrder = new (int,int)[9];
            switch (targetPriority)
            {
                case (TargetPriorityType.Closest):
                {
                    switch (sourcePosition.y)
                    {
                        case 0:
                            sortOrder = FIRST_ROW_SORT_ORDER;
                            break;
                        case 1:
                            sortOrder = SECOND_ROW_SORT_ORDER;
                            break;
                        case 2:
                            sortOrder = THIRD_ROW_SORT_ORDER;
                            break;
                        default:
                            Debug.LogError("Invalid source position!");
                            break;
                    }

                    for (int i = 0; i < sortOrder.Length; i++)
                    {
                        Vector2 targetPosition = new Vector2(sortOrder[i].Item1, sortOrder[i].Item2);
                        var tempTarget = possibleTargets.Find(x => x.Position == targetPosition);
                        if (tempTarget != null)
                        {
                            sortedTargets.Add(tempTarget);
                        }
                    }
                    possibleTargets.Clear();
                    possibleTargets.AddRange(sortedTargets);
                    break;
                }
                case (TargetPriorityType.LowestHealth):
                {
                    possibleTargets = possibleTargets.OrderBy(cell => 
                        cell.Entity.GetEntityComponent<HealthComponent>().Value).ToList();
                    break;
                }
                case (TargetPriorityType.Random):
                {
                    int i = possibleTargets.Count;
                    while (i > 1) 
                    {  
                        i--;  
                        int k = Random.Range(0, i+1);
                        (possibleTargets[k], possibleTargets[i]) = (possibleTargets[i], possibleTargets[k]);
                    } 
                    break;
                }
            }
        }
        

        // public List<IEntity> GetTargets(Team activeTeam, 
        //     Vector2 sourcePosition, TargetType targetType = TargetType.EnemyTarget, int targetsCount = 1)
        // {
        //     List<IEntity> targetEntities = new();
        //     switch (targetType)
        //     {
        //         case (TargetType.EnemyTarget):
        //         {
        //             int sourceRow = (int)sourcePosition.y;
        //             TeamGridModel targetTeam = activeTeam == Team.Left ? _rightGridModel : _leftGridModel;
        //             //Get first entity in this row
        //             IEntity targetEntity = GetFrontEntity(targetTeam, sourceRow);
        //             targetEntities.Add(targetEntity);
        //             //Get first entity in 1st row
        //             //Get first entity in 3rd row
        //             
        //             //var possibleTargets = targetTeam == Team.Left ? GetLeftTeamEntities() : GetRightTeamEntities();
        //             
        //             break;
        //         }
        //         default:
        //         {
        //             Debug.LogError($"Invalid target type: {targetType}");
        //             break;
        //         }
        //     }
        //     return targetEntities;
        // }

        // private IEntity GetFrontEntity(TeamGridModel targetTeam, int sourceRow)
        // {
        //     IEntity targetEntity = null;
        //     int checkedRowCount = 0;
        //     int nextRow = sourceRow;
        //     while (targetEntity == null && checkedRowCount < 3)
        //     {
        //         if (!targetTeam.TryGetFrontEntityInRow(nextRow, out targetEntity))
        //         {
        //             if (nextRow == sourceRow)
        //             {
        //                 if (sourceRow == 0)
        //                 {
        //                     nextRow++;
        //                 }
        //                 else
        //                 {
        //                     nextRow--;
        //                 }
        //             }
        //             else
        //             {
        //                 if (nextRow == 1)
        //                 {
        //                     nextRow--;
        //                 }
        //                 else
        //                 {
        //                     nextRow = 2;
        //                 }
        //             }
        //             checkedRowCount++;
        //         }
        //     }
        //
        //     if (targetEntity == null)
        //     {
        //         Debug.LogError("Target entity not found!");
        //     }
        //     return targetEntity;
        // }
        
        // private bool TryGetFrontTarget(List<IEntity> possibleTargets, int sourceRow, out IEntity entity)
        // {
        //     IEntity targetEntity = null;
        //     int checkedRowCount = 0;
        //     int nextRow = sourceRow;
        //     while (targetEntity == null && checkedRowCount < 3)
        //     {
        //         if (!targetTeam.TryGetFrontEntityInRow(nextRow, out targetEntity))
        //         {
        //             if (nextRow == sourceRow)
        //             {
        //                 if (sourceRow == 0)
        //                 {
        //                     nextRow++;
        //                 }
        //                 else
        //                 {
        //                     nextRow--;
        //                 }
        //             }
        //             else
        //             {
        //                 if (nextRow == 1)
        //                 {
        //                     nextRow--;
        //                 }
        //                 else
        //                 {
        //                     nextRow = 2;
        //                 }
        //             }
        //             checkedRowCount++;
        //         }
        //     }
        //
        //     if (targetEntity == null)
        //     {
        //         Debug.LogError("Target entity not found!");
        //     }
        //     return targetEntity;
        // }
    }
}