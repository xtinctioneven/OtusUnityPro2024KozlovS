﻿using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Gameplay;
using UnityEngine;

public class MoveIntoAbilityPositionVisualTask : EventTask
{
    private readonly AbilityVisualData _abilityVisualData;
    private readonly IEntity _sourceEntity;
    private readonly List<IEntity> _targetEntities;
    private readonly VisualPipeline _visualPipeline;

    public MoveIntoAbilityPositionVisualTask(AbilityVisualData visualData, IEntity source, List<IEntity> targets, VisualPipeline visualPipeline)
    {
        _sourceEntity = source;
        _abilityVisualData = visualData;
        _targetEntities = targets;
        _visualPipeline = visualPipeline;
    }

    protected override void OnRun()
    {
        var sourceAnimatorComponent = _sourceEntity.GetEntityComponent<AnimatorComponent>();
        switch (_abilityVisualData.CastType)
        {
            case (AbilityVisualData.AbilityCastType.Melee):
            {
                IEntity target = _targetEntities[0];
                GridPositionComponent targetGridPositionComponent = target.GetEntityComponent<GridPositionComponent>();
                ViewComponent sourceViewComponent = _sourceEntity.GetEntityComponent<ViewComponent>();
                float offset = _abilityVisualData.MeelePositionOffset;
                if (target.GetEntityComponent<TeamComponent>().Value == Team.Right)
                {
                    offset *= -1;
                }
                Vector3 targetPosition = targetGridPositionComponent.WorldGridPosition;
                targetPosition.z += offset;
                if (sourceViewComponent.Position == targetPosition)
                {
                    Finish();
                }
                else
                {
                    _sourceEntity.GetEntityComponent<AnimatorComponent>().PlayAnimation("MoveForward");
                    sourceViewComponent.Value.transform.DOJump(targetPosition, 2f, 1, .3f)
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
        _visualPipeline.AddTask(new MoveBackVisualTask(_sourceEntity, _abilityVisualData));
        Finish();
    }
}