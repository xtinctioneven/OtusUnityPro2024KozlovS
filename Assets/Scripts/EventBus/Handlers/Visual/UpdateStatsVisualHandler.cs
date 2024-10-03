using UnityEngine;

public class UpdateStatsVisualHandler : BaseHandler<UpdateStatsEvent>
{
    private readonly VisualPipeline _visualPipeline;
    
    public UpdateStatsVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void OnHandleEvent(UpdateStatsEvent evt)
    {
        HeroEntity hero = evt.HeroEntity;
        LifeComponent lifeComponent = hero.GetHeroComponent<LifeComponent>();
        AudioClip audioClip = null;
        float lifeCoefficient = (float)lifeComponent.Value / lifeComponent.MaxValue; 
        if (lifeCoefficient <= .2f)
        {
            audioClip = lifeCoefficient <= 0 ? hero.GetHeroComponent<AudioComponent>().GetDeathSound() 
                : hero.GetHeroComponent<AudioComponent>().GetLowHealthSound();
        }
        string newStatsValue = $"<color=blue>{evt.AttackValue}</color> / <color=red>{evt.HPValue}</color>"; 
        _visualPipeline.AddTask(new UpdateStatsVisualTask(hero.GetHeroComponent<ViewComponent>().Value, newStatsValue, audioClip));
    }
}