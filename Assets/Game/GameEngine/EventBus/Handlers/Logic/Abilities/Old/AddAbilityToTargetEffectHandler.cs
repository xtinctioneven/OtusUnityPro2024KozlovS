using System.Collections.Generic;

public class AddAbilityToTargetEffectHandler: BaseHandler<AddAbilityToTargetEffectOld>
{
    private readonly VisualPipeline _visualPipeline;
    
    public AddAbilityToTargetEffectHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(AddAbilityToTargetEffectOld evt)
    {
        HeroInteractionData interactionData = evt.HeroInteractionData;
        HeroEntity sourceHero = interactionData.SourceHero;
        HeroEntity targetHero = interactionData.TargetHero;
        if (sourceHero != evt.SourceHero || evt.SourceHero == null)
        {
            return;
        }
        // List<IEffectOld> targetAbilities = targetHero.GetHeroComponent<AbilityComponent>().GetAbilities();
        // targetAbilities.Add(evt.AbilityToGive);
        if (evt.VfxAbilityData.Vfx)
        {
            _visualPipeline.AddTask(new PlayVfxTask(evt.VfxAbilityData, sourceHero, targetHero));
        }

        if (evt.AbilityToGive.VfxAbilityData.Vfx)
        {
            _visualPipeline.AddTask(new ActivateAbilityVfxTask(targetHero, evt.AbilityToGive));
        }
    }
}