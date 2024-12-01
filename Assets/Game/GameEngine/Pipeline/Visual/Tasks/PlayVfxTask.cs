using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PlayVfxTask : EventTask
{
    public const float VFX_SORT_ADJUST = 5.0f;
    private const int VFX_DEFAULT_DURATION = 1500;
    private readonly VfxData _vfxData;
    private ParticleSystem _vfx;
    private readonly HeroEntity _sourceHero;
    private readonly HeroEntity _targetHero;

    public PlayVfxTask(VfxData vfxData, HeroEntity sourceHero, HeroEntity targetHero = null)
    {
        _vfxData = vfxData;
        _sourceHero = sourceHero;
        if (targetHero != null)
        {
            _targetHero = targetHero;
        }
    }

    protected override void OnRun()
    {
        if (_sourceHero.GetHeroComponent<ViewComponentOld>().Value.enabled == false)
        {
            Finish();
        }
        //Vector3 targetPosition = Vector3.up;
        TryPlayAudio();
        switch (_vfxData.TargetType)
        {
            case VfxData.VfxTargetType.AtSelf:
            {
                SpawnVfx(_sourceHero.GetHeroComponent<VisualAnchorComponent>().Value);
                //PlayAwaitableVfx(_vfxData.Duration);
                break;
            }
            case VfxData.VfxTargetType.AtTarget:
            {
                SpawnVfx(_targetHero.GetHeroComponent<VisualAnchorComponent>().Value);
                //PlayAwaitableVfx(_vfxData.Duration);
                break;
            }
            case VfxData.VfxTargetType.AtSelfRotatedToTarget:
            {
                VisualAnchorComponent selfVisualAnchor = _sourceHero.GetHeroComponent<VisualAnchorComponent>();
                SpawnVfx(selfVisualAnchor.Value);
                RotateVfxToTarget(selfVisualAnchor.Position, _targetHero.GetHeroComponent<VisualAnchorComponent>().Position);
                //PlayAwaitableVfx(_vfxData.Duration);
                break;
            }
            case VfxData.VfxTargetType.AtSelfMovingToTarget:
            {
                SpawnVfx(_sourceHero.GetHeroComponent<VisualAnchorComponent>().Value);
                // targetPosition = _targetHero.GetHeroComponent<VisualAnchorComponent>().Value.position;
                // targetPosition.z = VFX_SORT_ADJUST;
                //PlayAwaitableMovingVfx(targetPosition, _vfxData.Duration);
                break;
            }
            default:
            {
                Debug.LogError($"VfxData.TargetType: {_vfxData.TargetType}");
                break;
            }
        }
    }

    private void RotateVfxToTarget(Vector3 selfPosition, Vector3 targetPosition)
    {
        Vector3 targetDirection = targetPosition - selfPosition;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        _vfx.transform.rotation = targetRotation;
    }

    private void SpawnVfx(Transform parent)
    {
        _vfx = Object.Instantiate(_vfxData.Vfx, parent, true);
        _vfx.transform.localPosition = Vector3.zero;
        _vfx.transform.rotation = Quaternion.identity;
    }


    // private async UniTask PlayAwaitableMovingVfx(Vector3 targetPosition, int duration)
    // {
    //     if (duration <= 0)
    //     {
    //         duration = VFX_DEFAULT_DURATION;
    //     }
    //     _vfx.Play();
    //     _vfx.transform.DOMove(targetPosition,(float)duration/1000).SetEase(Ease.OutQuint).OnComplete(() =>
    //     {
    //         Object.Destroy(_vfx.gameObject);
    //         Finish();
    //     });
    // }
    

    private async UniTask PlayAwaitableVfx(int duration)
    {
        if (duration <= 0)
        {
            duration = VFX_DEFAULT_DURATION;
        }
        _vfx.Play();
        await UniTask.Delay(duration);
        Object.Destroy(_vfx.gameObject);
        Finish();
    }

    private void TryPlayAudio()
    {
        AudioClip audioClip = _sourceHero.GetHeroComponent<AudioComponent>().GetAbilityCast();
        if (audioClip != null)
        {
            AudioPlayer.Instance.PlaySound(audioClip);
        }
    }
}