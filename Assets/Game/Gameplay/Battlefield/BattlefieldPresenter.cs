using Game.Gameplay;
using UnityEngine;
using Zenject;

public class BattlefieldPresenter : MonoBehaviour
{
    [SerializeField] private TeamGridPresenter _leftTeamGridPresenter;
    [SerializeField] private TeamGridPresenter _rightTeamGridPresenter;
    private BattlefieldModel _battlefieldModel;

    public void Setup(BattlefieldModel battlefieldModel)
    {
        _battlefieldModel = battlefieldModel;
        _battlefieldModel.OnEntitySetup += BattlefieldModelOnOnEntitySetup;
        _leftTeamGridPresenter.Setup(_battlefieldModel.LeftGridModel);
        _rightTeamGridPresenter.Setup(_battlefieldModel.RightGridModel);
    }

    private void BattlefieldModelOnOnEntitySetup(IEntity entity, Vector2 gridPosition)
    {
        Team team = entity.GetEntityComponent<TeamComponent>().Value;
        EntityViewComponent entityViewComponent = entity.GetEntityComponent<EntityViewComponent>();
        Vector3 worldPosition;
        Quaternion worldRotation;
        if (team == Team.Left)
        {
            worldPosition = _leftTeamGridPresenter.GetPositionOfGridPosition(gridPosition);
            worldRotation = _leftTeamGridPresenter.GetRotationOfGridPosition(gridPosition);
        }
        else
        {
            worldPosition = _rightTeamGridPresenter.GetPositionOfGridPosition(gridPosition);
            worldRotation = _rightTeamGridPresenter.GetRotationOfGridPosition(gridPosition);
        }
        entityViewComponent.SetPosition(worldPosition);
        entityViewComponent.SetRotation(worldRotation);
        entity.GetEntityComponent<GridPositionComponent>().SetWorldGridPosition(worldPosition);
    }
}