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
            case (AbilityVisualData.AbilityCastType.Melee):
            {
                ViewComponent sourceViewComponent = _sourceEntity.GetEntityComponent<ViewComponent>();
                float offset = _abilityVisualData.MeelePositionOffset;
                Vector3 targetPosition = _sourceEntity.GetEntityComponent<GridPositionComponent>().WorldGridPosition;
                if (sourceViewComponent.Position == targetPosition)
                {
                    Finish();
                }
                else
                {
                    _sourceEntity.GetEntityComponent<AnimatorComponent>().PlayAnimation("MoveBack");
                    _sourceEntity.GetEntityComponent<ViewComponent>().Value.transform.DOJump(targetPosition, 2f, 1, .3f)
                        .SetEase(Ease.OutSine).OnComplete(OnMoveFinish);
                }
                break;
            }
            case (AbilityVisualData.AbilityCastType.FromPlace):
            {
                Finish();
                break;
            }
            case (AbilityVisualData.AbilityCastType.SameRowMelee):
            {
                //TODO: Extension
                break;
            }
            case (AbilityVisualData.AbilityCastType.SameRowRanged):
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