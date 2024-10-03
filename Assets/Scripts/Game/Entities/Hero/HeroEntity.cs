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
        AttackComponent attackComponent = new AttackComponent(config.AttackValue);
        LifeComponent lifeComponent = new LifeComponent(config.HealthValue);
        AbilityComponent abilityComponent = new AbilityComponent(config.Ability);
        TeamComponent teamComponent = new TeamComponent(heroModel.Team);
        TransformComponent transformComponent = new TransformComponent(transform);
        DeathComponent deathComponent = new DeathComponent();
        DestroyComponent destroyComponent = new DestroyComponent(this.gameObject);
        VfxComponent vfxComponent = new VfxComponent();
        ViewComponent viewComponent =
            new ViewComponent(GetComponent<HeroView>());
        AudioComponent audioComponent = new AudioComponent(config.TurnStartSounds, config.LowHealthSounds, 
            config.AbilityCastSounds, config.DeathSounds);
        VisualAnchorComponent anchorComponent = new VisualAnchorComponent(viewComponent.Value.GetHeroImage().transform);

        
        _components.Add(typeof(AttackComponent), attackComponent);
        _components.Add(typeof(LifeComponent), lifeComponent);
        _components.Add(typeof(AbilityComponent), abilityComponent);
        _components.Add(typeof(TeamComponent), teamComponent);
        _components.Add(typeof(TransformComponent), transformComponent);
        _components.Add(typeof(DeathComponent), deathComponent);
        _components.Add(typeof(DestroyComponent), destroyComponent);
        _components.Add(typeof(VfxComponent), vfxComponent);
        _components.Add(typeof(ViewComponent), viewComponent);
        _components.Add(typeof(AudioComponent), audioComponent);
        _components.Add(typeof(VisualAnchorComponent), anchorComponent);

        viewComponent.Value.SetStats($"<color=blue>{attackComponent.Value}</color> / <color=red>{lifeComponent.Value}</color>");
        abilityComponent.Install();
        vfxComponent.Install(abilityComponent.GetAbilities(), anchorComponent.Value);
        viewComponent.Value.gameObject.name += $" {config.HeroName}";
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
