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
    }

    private void BattlefieldModelOnOnEntitySetup(IEntity entity, Vector2 gridPosition)
    {
        Team team = entity.GetEntityComponent<TeamComponent>().Value;
        ViewComponent viewComponent = entity.GetEntityComponent<ViewComponent>();
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
        viewComponent.SetPosition(worldPosition);
        viewComponent.SetRotation(worldRotation);
    }
}