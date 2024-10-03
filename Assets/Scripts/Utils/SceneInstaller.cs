using System;
using UI;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    //public const string RED_TEAM = nameof(RED_TEAM);
    //public const string BLUE_TEAM = nameof(BLUE_TEAM);
    // const string RED_TEAM = Enum.GetName(typeof(Team), Team.RedTeamTeam);
    // [SerializeField] private HeroListView _redHeroListView;
    // [SerializeField] private HeroListView _blueHeroListView;
    
    public override void InstallBindings()
    {
        ConfigureEventBus();
        ConfigureHandlers();
        ConfigureServices();
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
        Container.BindInterfacesAndSelfTo<ActivateHeroHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DeathHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameOverHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<HeroInteractionHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TargetHeroClickedHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UpdateStatsHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<HeroStatsResolveHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ActivateAbilitiesHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<RemoveAbilityHandler>().FromNew().AsSingle().NonLazy();
        
        //Logic/Effects
        Container.BindInterfacesAndSelfTo<DamageRandomEnemyEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<NullifyRetaliateDamageEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SetRandomTargetEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<StealHealthOnAttackEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<NullifyDamageEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AddAbilityToTargetEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SkipTurnEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<HealRandomAllyEffectHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DamageAllEffectHandler>().FromNew().AsSingle().NonLazy();
        
        //Visual
        Container.BindInterfacesAndSelfTo<ActivateHeroVisualHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AttackVisualHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DeactivateHeroVisualHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DeathVisualHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameOverVisualHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UpdateStatsVisualHandler>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureServices()
    {
        //Register services
        Container.BindInterfacesAndSelfTo<UIService>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TurnOrderService>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<HeroTrackerService>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<HeroInteractionService>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureTurnPipeline()
    {
        //Register turn pipeline
        //Container.BindInterfacesAndSelfTo<TurnPipelineRunner>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TurnPipeline>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TurnPipelineInstaller>().FromNew().AsSingle().NonLazy();
    }
    
    private void ConfigureVisualPipeline()
    {
        //Register visual pipeline
        Container.BindInterfacesAndSelfTo<VisualPipeline>().FromNew().AsSingle().NonLazy();
    }
}