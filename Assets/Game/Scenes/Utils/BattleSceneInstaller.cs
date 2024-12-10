using System;
using Game.Gameplay;
using UI;
using UnityEngine;
using Zenject;

public class BattleSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        ConfigureSceneContext();
        ConfigureEventBus();
        ConfigureHandlers();
        ConfigureServices();
        ConfigureBattlePipeline();
        ConfigureRoundPipeline();
        ConfigureTurnPipeline();
        ConfigureVisualPipeline();
    }

    private void ConfigureSceneContext()
    {
        Container.BindInterfacesAndSelfTo<BattlefieldPresenter>().FromComponentsInHierarchy().AsSingle().NonLazy();
    }
    
    private void ConfigureEventBus()
    {
        Container.BindInterfacesAndSelfTo<EventBus>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureHandlers()
    {
        //Register handlers
        //Logic
        Container.BindInterfacesAndSelfTo<RemoveStatusEffectEventHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EntityInteractionHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UpdateStatsHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DeathHandler>().FromNew().AsSingle().NonLazy();
        
        //Logic/Effects
        Container.BindInterfacesAndSelfTo<StrikeTargetEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<StrikeTargetLinkEffecttHandler>().FromNew().AsSingle().NonLazy();
        
        //Logic/Effects/Triggers
        Container.BindInterfacesAndSelfTo<HealTargetTriggerEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AddCountToStandardTriggerEffectHandler>().FromNew().AsSingle().NonLazy();
        //Logic/Effects/Passives
        Container.BindInterfacesAndSelfTo<AddCountToSourceOnResultPassiveEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EvasionPassiveEffectHandler>().FromNew().AsSingle().NonLazy();
        //Logic/Effects/StatusEffects
        Container.BindInterfacesAndSelfTo<DamageMaxHpPercentStatusEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AddCountToSourceStatusEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CleanseStatusEffectHandler>().FromNew().AsSingle().NonLazy();
        
        //Visual
        // Container.BindInterfacesAndSelfTo<ActivateHeroVisualHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<AttackVisualHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<DeactivateHeroVisualHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DeathVisualHandler>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<GameOverVisualHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UpdateStatsVisualHandler>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureServices()
    {
        //Register services
        Container.BindInterfacesAndSelfTo<CharacterFactory>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EntityTrackerService>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TurnOrderService>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EntityInteractionService>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TargetFinderService>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LinkEffectsTracker>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AbilityService>().FromNew().AsSingle().NonLazy();
        
        
        // Container.BindInterfacesAndSelfTo<UIService>().FromComponentInHierarchy().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<TurnOrderService>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<HeroTrackerService>().FromNew().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<HeroInteractionService>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureTurnPipeline()
    {
        //Register turn pipeline
        Container.BindInterfacesAndSelfTo<TurnPipeline>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TurnPipelineInstaller>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureBattlePipeline()
    {
        //Register battle pipeline
        Container.BindInterfacesAndSelfTo<BattlePipeline>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<BattlePipelineInstaller>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureRoundPipeline()
    {
        //Register battle pipeline
        Container.BindInterfacesAndSelfTo<RoundPipeline>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<RoundPipelineInstaller>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureVisualPipeline()
    {
        //Register visual pipeline
        Container.BindInterfacesAndSelfTo<VisualPipeline>().FromNew().AsSingle().NonLazy();
    }
}