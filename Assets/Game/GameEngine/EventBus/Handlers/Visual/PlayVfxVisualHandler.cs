using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PlayVfxVisualHandler: BaseHandler<PlayVfxEvent>
{
    private VfxData _vfxData;
    private VfxAnchorComponent _sourceAnchorComponent;
    private VfxAnchorComponent _targetAnchorComponent;
    
    public PlayVfxVisualHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void OnHandleEvent(PlayVfxEvent evt)
    { 
        _vfxData = evt.InteractionData.SourceEffect.AbilityVisualData.AbilityVfxData;
        _sourceAnchorComponent = evt.InteractionData.SourceEntity.GetEntityComponent<VfxAnchorComponent>();
        _targetAnchorComponent = evt.InteractionData.TargetEntity.GetEntityComponent<VfxAnchorComponent>();
        switch (_vfxData.AppearanceType)
        {
            case (VfxAppearanceType.CastOnSelf):
            {
                //ВФХ появляется на своем таргет
                Vector3 position = _sourceAnchorComponent.CastTarget.position;
                var vfx = GameObject.Instantiate(_vfxData.Prefab, position, Quaternion.identity);
                UniTask.WaitForSeconds(_vfxData.CollisionDelay);
                GameObject.Destroy(vfx.gameObject);
                break;
            }
            case (VfxAppearanceType.CastOnTarget):
            {
                //ВФХ появляется на таргете таргета
                Vector3 position = _targetAnchorComponent.CastTarget.position;
                var vfx = GameObject.Instantiate(_vfxData.Prefab, position, Quaternion.identity);
                UniTask.WaitForSeconds(_vfxData.CollisionDelay);
                GameObject.Destroy(vfx.gameObject);
                break;
            }
            case (VfxAppearanceType.TravelToTarget):
            {
                Vector3 startPosition = _sourceAnchorComponent.CastSource.position;
                Vector3 destination = _targetAnchorComponent.CastTarget.position;
                var vfx = GameObject.Instantiate(_vfxData.Prefab, startPosition, Quaternion.identity);
                vfx.transform.DOJump(destination, _vfxData.JumpPower, 1, _vfxData.CollisionDelay)
                    .OnComplete(() => GameObject.Destroy(vfx.gameObject));
                //ВФХ появляется на своем сорс и летит в таргет таргет
                break;
            }
            default:
            {
                Debug.LogError("Unhandled VFX appearance type");
                break;
            }
        }
    }
}