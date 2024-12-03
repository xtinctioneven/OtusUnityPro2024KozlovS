using System;
using Game.Gameplay;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    public BattlePipelineRunner _runner;
    private DiContainer _diContainer;
    
    [Inject]
    public void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }
    
    private void Start()
    {
        //1. Prepare Characters data -- from Helper
        TeamGridData[] leftTeamGridData = Helper.Instance.LeftTeamGridData;
        TeamGridData[] rightTeamGridData = Helper.Instance.RightTeamGridData;
        //2. Prepare Battlefield
        BattlefieldModel battlefieldModel = new BattlefieldModel(leftTeamGridData, rightTeamGridData);
        battlefieldModel.Print();
        //battlefieldModel.Print();
        _diContainer.Bind<BattlefieldModel>().FromInstance(battlefieldModel).AsSingle().NonLazy();
        //3. Run pipeline
        _runner.Run();
    }
}