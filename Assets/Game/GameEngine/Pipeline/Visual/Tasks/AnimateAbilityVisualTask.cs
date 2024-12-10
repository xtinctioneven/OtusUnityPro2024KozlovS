using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Gameplay;
using UnityEngine;

public class AnimateAbilityVisualTask : EventTask
{
    private EntityInteractionData _interactionData;
    private AbilityVisualData _abilityVisualData;
    private IEntity _sourceEntity;
    private IEntity _targetEntity;
    private AnimatorComponent _sourceAnimatorComponent;
    private AnimatorComponent _targetAnimatorComponent;
    private EventBus _eventBus;

    public AnimateAbilityVisualTask(EntityInteractionData interactionData, EventBus eventBus)
    {
        _eventBus = eventBus;
        _interactionData = interactionData;
        _sourceEntity = interactionData.SourceEntity;
        _targetEntity = interactionData.TargetEntity != null ? interactionData.TargetEntity : null;
        _abilityVisualData = interactionData.SourceEffect.AbilityVisualData;
        _sourceAnimatorComponent = _sourceEntity.GetEntityComponent<AnimatorComponent>();
        if (_targetEntity != null)
        {
            _targetAnimatorComponent = _targetEntity.GetEntityComponent<AnimatorComponent>();
        }
    }

    protected override void OnRun()
    {
        string animationTrigger = _abilityVisualData.SourceAnimationClip.ToString();
        if (animationTrigger == "None")
        {
            Finish();
            return;
        }

        switch (_abilityVisualData.CollisionType)
        {
            case (AbilityCollisionType.None):
            {
                Finish();
                return;
            }
            case (AbilityCollisionType.Strike):
            {
                _sourceAnimatorComponent.OnAnimationEnd += OnAnimationEnd;
                _sourceAnimatorComponent.OnStrike += OnAttackConnected;
                _sourceAnimatorComponent.PlayAnimation(animationTrigger);
                break;
            }
            case (AbilityCollisionType.Cast):
            {
                if (_abilityVisualData.AbilityVfxData.Prefab != null)
                {
                    _eventBus.RaiseEvent(new PlayVfxEvent(_interactionData));
                }
                _sourceAnimatorComponent.OnAnimationEnd += OnAnimationEnd;
                _sourceAnimatorComponent.OnCast += OnCast;
                _sourceAnimatorComponent.PlayAnimation(animationTrigger);
                break;
            }
            default:
            {
                Debug.LogError($"Unknown Collision Type: {_abilityVisualData.CollisionType}");
                break;
            }
        }
    }

    private void OnAttackConnected()
    {
        if (_abilityVisualData.AbilityVfxData.Prefab != null && _abilityVisualData.CollisionType != AbilityCollisionType.Cast)
        {
            _eventBus.RaiseEvent(new PlayVfxEvent(_interactionData));
        }
        _sourceAnimatorComponent.OnStrike -= OnAttackConnected;
        var interactionResult = _interactionData.InteractionResult;
        if (interactionResult == InteractionResult.Dodge)
        {
            _targetAnimatorComponent.PlayAnimation(AnimationClipType.Dodge.ToString());
            return;
        }

        if (interactionResult != InteractionResult.Hit
            && interactionResult != InteractionResult.CriticalHit
            && interactionResult != InteractionResult.Kill)
        {
            return;
        }

        var linkStatus = LinkStatusType.None;
        foreach (var statusEffect in _interactionData.StatusEffectsApplyToTarget)
        {
            if (statusEffect is LinkStatusEffect linkStatusEffect)
            {
                linkStatus = linkStatusEffect.LinkStatus;
            }
        }
        switch (linkStatus)
        {
            case (LinkStatusType.None):
            {
                _targetAnimatorComponent.PlayAnimation(AnimationClipType.Hit.ToString());
                _targetEntity.GetEntityComponent<EntityViewComponent>().Value.transform.
                    DOPunchPosition(Vector3.up * Helper.Instance.NoLinkWigglePower, Helper.Instance.NoLinkWiggleDuration);
                break;
            }
            case (LinkStatusType.Repulse):
            {
                _targetAnimatorComponent.PlayAnimation(linkStatus.ToString());
                Vector3 targetPosition = _targetEntity.GetEntityComponent<GridPositionComponent>().WorldGridPosition;
                targetPosition.z = _targetEntity.GetEntityComponent<TeamComponent>().Value == Team.Left 
                    ? Helper.Instance.RepulseLeftTeamZ 
                    : Helper.Instance.RepulseRightTeamZ;
                _targetEntity.GetEntityComponent<EntityViewComponent>().Value.transform
                    .DOMove(targetPosition, Helper.Instance.LinkRepulseDuration)
                    .SetEase(Helper.Instance.RepulseEase).SetLoops(2, LoopType.Yoyo);
                break;
            }
            case (LinkStatusType.LowFloat):
            {
                _targetAnimatorComponent.PlayAnimation(linkStatus.ToString());
                _targetEntity.GetEntityComponent<EntityViewComponent>().Value.transform
                    .DOMoveY(Helper.Instance.LinkLowFloatAltitude, Helper.Instance.LinkLowFloatDuration)
                    .SetEase(Helper.Instance.LowFloatEase).SetLoops(2, LoopType.Yoyo);;
                break;
            }
            case (LinkStatusType.HighFloat):
            {
                _targetAnimatorComponent.PlayAnimation(linkStatus.ToString());
                _targetEntity.GetEntityComponent<EntityViewComponent>().Value.transform
                    .DOMoveY(Helper.Instance.LinkHighFloatAltitude, Helper.Instance.LinkHighFloatDuration)
                    .SetEase(Helper.Instance.HighFloatEase).SetLoops(2, LoopType.Yoyo);
                break;
            }
        }
    }

    private async void OnCast()
    {
        //TODO: Fire VFX!
        _sourceAnimatorComponent.OnCast -= OnCast; 
        await UniTask.WaitForSeconds(_abilityVisualData.AbilityVfxData.CollisionDelay);
        OnAttackConnected();
    }

    private void OnAnimationEnd()
    {
        _sourceAnimatorComponent.OnAnimationEnd -= OnAnimationEnd;
        Finish();
    }
}