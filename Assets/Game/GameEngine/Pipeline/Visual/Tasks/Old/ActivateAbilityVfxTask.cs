using UnityEngine;

public class ActivateAbilityVfxTask : EventTask
{
    private readonly HeroEntity _heroEntity;
    // private readonly IEffectOld _ability;
    private ParticleSystem _vfx;

    public ActivateAbilityVfxTask(HeroEntity heroEntity 
        // IEffectOld ability
        )
    {
        _heroEntity = heroEntity;
        // _ability = ability;
    }

    protected override void OnRun()
    {
        // VfxData vfxData = _ability.VfxAbilityData;
        Transform vfxParent = _heroEntity.GetHeroComponent<VisualAnchorComponent>().Value;
        // _vfx = Object.Instantiate(vfxData.Vfx, vfxParent, true);
        _vfx.transform.localPosition = Vector3.zero;
        _vfx.transform.rotation = Quaternion.identity;
        _vfx.Play();
        // if (!_heroEntity.GetHeroComponent<VfxComponent>().TryAddVfxByAbility(_ability, _vfx))
        {
            Debug.LogError("<color=red>Vfx could not be added to VfxComponent</color>");
        }
        Finish();
    }
}