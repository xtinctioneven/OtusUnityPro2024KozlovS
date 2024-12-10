using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Gameplay;
using UnityEngine;

public class MoveBackVisualTask : EventTask
{
    private readonly IEntity _sourceEntity;
    private readonly AbilityVisualData _abilityVisualData;

    public MoveBackVisualTask(IEntity source, AbilityVisualData abilityVisualData)
    {
        _sourceEntity = source;
        _abilityVisualData = abilityVisualData;
    }

    protected override void OnRun()
    {
        switch (_abilityVisualData.CastType)
        {
            case (AbilityCastType.Melee):
            {
                EntityViewComponent sourceEntityViewComponent = _sourceEntity.GetEntityComponent<EntityViewComponent>();
                float offset = _abilityVisualData.MeelePositionOffset;
                Vector3 targetPosition = _sourceEntity.GetEntityComponent<GridPositionComponent>().WorldGridPosition;
                if (sourceEntityViewComponent.Position == targetPosition)
                {
                    Finish();
                }
                else
                {
                    _sourceEntity.GetEntityComponent<AnimatorComponent>().PlayAnimation("MoveBack");
                    sourceEntityViewComponent.Value.transform.DOJump(targetPosition, Helper.Instance.BackJumpPower,
                            1, Helper.Instance.BackJumpDuration)
                        .SetEase(Helper.Instance.BackJumpEase).OnComplete(OnMoveFinish);
                }
                break;
            }
            case (AbilityCastType.FromPlace):
            {
                Finish();
                break;
            }
            case (AbilityCastType.SameRowMelee):
            {
                //TODO: Extension
                break;
            }
            case (AbilityCastType.SameRowRanged):
            {
                //TODO: Extension
                break;
            }
            default:
            {
                Debug.LogError($"Unhandled ability Cast Type: {_abilityVisualData.CastType}");
                break;
            }
        }
            
    }

    private void OnMoveFinish()
    {
        Finish();
    }
}