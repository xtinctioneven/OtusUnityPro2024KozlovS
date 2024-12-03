using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay
{
    public class BattlefieldModel
    {
        public TeamGridModel LeftGridModel { get; private set; }
        public TeamGridModel RightGridModel  { get; private set; }

        public BattlefieldModel(TeamGridData[] leftTeamGridData = null, TeamGridData[] rightTeamGridData = null)
        {
            LeftGridModel = new TeamGridModel(Team.Left, leftTeamGridData);
            RightGridModel = new TeamGridModel(Team.Right, rightTeamGridData);
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
            return RightGridModel.GetAllEntities().ToList();
        }

        public List<IEntity> GetLeftTeamEntities()
        {
            return LeftGridModel.GetAllEntities().ToList();
        }

        public void Print()
        {
            Debug.Log("Left team:");
            LeftGridModel.Print();
            Debug.Log("Right team:");
            RightGridModel.Print();
        }
    }
}