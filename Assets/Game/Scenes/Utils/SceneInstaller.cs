using System;
using Game.Gameplay;
using UI;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CharacterFactory>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<StrikeTargetEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EvasionPassiveEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<HealTargetTriggerEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AddCountToSourceOnResultPassiveEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AddCountToStandardTriggerEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DamageMaxHpPercentStatusEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AddCountToSourceStatusEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CleanseStatusEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<RemoveStatusEffectEventHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EntityInteractionHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<StatsResolveHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UpdateStatsHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DeathHandler>().FromNew().AsSingle().NonLazy();
        
        //Container.BindInterfacesAndSelfTo<EntityTrackerService>().FromNew().AsSingle().NonLazy();
        
        
        ConfigureEventBus();
        ConfigureHandlers();
        ConfigureServices();
        ConfigureBattlePipeline();
        ConfigureRoundPipeline();
        ConfigureTurnPipeline();
        ConfigureVisualPipeline();
    }

    private void ConfigureEventBus()
    {
        Container.BindInterfacesAndSelfTo<EventBus>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureHandlers()
    {
        //Register handlers
        //Logic
        // Container.BindInterfacesAndSelfTo<ActivateHeroHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<DeathHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<GameOverHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<HeroInteractionHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<TargetHeroClickedHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<UpdateStatsHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<HeroStatsResolveHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<ActivateAbilitiesHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<RemoveAbilityHandler>().FromNew().AsSingle().NonLazy();
        
        //Logic/Effects
        // Container.BindInterfacesAndSelfTo<DamageRandomEnemyEffectHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<NullifyRetaliateDamageEffectHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<SetRandomTargetEffectHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<StealHealthOnAttackEffectHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<NullifyDamageEffectHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<AddAbilityToTargetEffectHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<SkipTurnEffectHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<HealRandomAllyEffectHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<DamageAllEffectHandler>().FromNew().AsSingle().NonLazy();
        
        //Visual
        // Container.BindInterfacesAndSelfTo<ActivateHeroVisualHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<AttackVisualHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<DeactivateHeroVisualHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<DeathVisualHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<GameOverVisualHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<UpdateStatsVisualHandler>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureServices()
    {
        //Register services
        // Container.BindInterfacesAndSelfTo<UIService>().FromComponentInHierarchy().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<TurnOrderService>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<HeroTrackerService>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<HeroInteractionService>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureTurnPipeline()
    {
        //Register turn pipeline
        ////Container.BindInterfacesAndSelfTo<TurnPipelineRunner>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TurnPipeline>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TurnPipelineInstaller>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureBattlePipeline()
    {
        //Register battle pipeline
        //Container.BindInterfacesAndSelfTo<TurnPipelineRunner>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<BattlePipeline>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<BattlePipelineInstaller>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureRoundPipeline()
    {
        //Register battle pipeline
        //Container.BindInterfacesAndSelfTo<TurnPipelineRunner>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<RoundPipeline>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<RoundPipelineInstaller>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureVisualPipeline()
    {
        //Register visual pipeline
        //Container.BindInterfacesAndSelfTo<VisualPipeline>().FromNew().AsSingle().NonLazy();
    }
}