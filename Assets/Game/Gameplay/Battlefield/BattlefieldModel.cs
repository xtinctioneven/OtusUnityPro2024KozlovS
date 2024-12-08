using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    [Serializable]
    public class BattlefieldModel
    {
        public event Action<IEntity, Vector2> OnEntitySetup;
        public TeamGridModel LeftGridModel { get; private set; }
        public TeamGridModel RightGridModel { get; private set; }
        public BattlefieldPresenter BattlefieldPresenter { get; private set; }
        
        [Inject]
        public BattlefieldModel(
            [Inject(Id = "LeftTeamData")] TeamGridData[] leftTeamGridData,
            [Inject(Id = "RightTeamData")] TeamGridData[] rightTeamGridData,
            BattlefieldPresenter battlefieldPresenter,
            CharacterFactory characterFactory
            )
        {
            BattlefieldPresenter = battlefieldPresenter;
            LeftGridModel = new TeamGridModel(Team.Left);
            BattlefieldPresenter.Setup(this);
            for (int i = 0; i < leftTeamGridData.Length; i++)
            {
                TeamGridData teamGridData = leftTeamGridData[i];
                IEntity entity = characterFactory.CreateCharacter(teamGridData.CharacterConfig);
                LeftGridModel.SetupEntity(entity, teamGridData.Position);
                OnEntitySetup?.Invoke(entity, teamGridData.Position);
            }
            
            RightGridModel = new TeamGridModel(Team.Right);
            for (int i = 0; i < rightTeamGridData.Length; i++)
            {
                TeamGridData teamGridData = rightTeamGridData[i];
                IEntity entity = characterFactory.CreateCharacter(teamGridData.CharacterConfig);
                RightGridModel.SetupEntity(entity, teamGridData.Position);
                OnEntitySetup?.Invoke(entity, teamGridData.Position);
            }
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