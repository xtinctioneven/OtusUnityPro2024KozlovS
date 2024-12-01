using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

[RequireComponent(typeof(HeroModel))]
public class HeroEntity : MonoBehaviour
{
    private readonly Dictionary<Type, object> _components = new ();

    private void Start()
    {
        HeroModel heroModel = GetComponent<HeroModel>();
        HeroConfig config = heroModel.HeroConfig;
        //AttackComponent attackComponent = new AttackComponent(config.AttackValue);
        //HealthComponent healthComponent = new HealthComponent(config.HealthValue);
        // AbilityComponent abilityComponent = new AbilityComponent(config.Ability);
        // TeamComponent teamComponent = new TeamComponent(heroModel.Team);
        //TransformComponent transformComponent = new TransformComponent(transform);
        DeathComponent deathComponent = new DeathComponent();
        DestroyComponent destroyComponent = new DestroyComponent(this.gameObject);
        VfxComponent vfxComponent = new VfxComponent();
        ViewComponentOld viewComponentOld =
            new ViewComponentOld(GetComponent<HeroView>());
        AudioComponent audioComponent = new AudioComponent(config.TurnStartSounds, config.LowHealthSounds, 
            config.AbilityCastSounds, config.DeathSounds);
        VisualAnchorComponent anchorComponent = new VisualAnchorComponent(viewComponentOld.Value.GetHeroImage().transform);

        
        //_components.Add(typeof(AttackComponent), attackComponent);
        //_components.Add(typeof(HealthComponent), healthComponent);
        // _components.Add(typeof(AbilityComponent), abilityComponent);
        // _components.Add(typeof(TeamComponent), teamComponent);
        //_components.Add(typeof(TransformComponent), transformComponent);
        _components.Add(typeof(DeathComponent), deathComponent);
        _components.Add(typeof(DestroyComponent), destroyComponent);
        _components.Add(typeof(VfxComponent), vfxComponent);
        _components.Add(typeof(ViewComponentOld), viewComponentOld);
        _components.Add(typeof(AudioComponent), audioComponent);
        _components.Add(typeof(VisualAnchorComponent), anchorComponent);

        //iewComponentOld.Value.SetStats($"<color=blue>{attackComponent.Value}</color> / <color=red>{healthComponent.Value}</color>");
        // abilityComponent.Install();
        // vfxComponent.Install(abilityComponent.GetAbilities(), anchorComponent.Value);
        viewComponentOld.Value.gameObject.name += $" {config.HeroName}";
    }
    
    public TType GetHeroComponent<TType>()
    {
        return (TType)_components.GetValueOrDefault(typeof(TType));
    }
    
    public void RemoveComponent<TType>()
    {
        var component = _components.GetValueOrDefault(typeof(TType));
        if (component != null)
        {
            _components.Remove(typeof(TType));
        }
    }

    public void RemoveAllComponents()
    {
        _components.Clear();
    }
}
