using UnityEngine;

public class RemoveAbilityHandler: BaseHandler<RemoveAbilityEvent>
{
    private VisualPipeline _visualPipeline;
    
    public RemoveAbilityHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(RemoveAbilityEvent evt)
    {
        HeroEntity heroEntity = evt.HeroEntity;
        if (heroEntity.GetHeroComponent<VfxComponent>()
            .TryRemoveVfxByAbility(evt.Ability, out ParticleSystem abilityVfx))
        {
            _visualPipeline.AddTask(new DestoryVfxTask(abilityVfx));
        }
        heroEntity.GetHeroComponent<AbilityComponent>().TryRemoveAbility(evt.Ability);
    }
}