using Zenject;

public class BattlePipelineInstaller: IInitializable
{
    private readonly DiContainer _diContainer;
    private readonly BattlePipeline _battlePipeline;
    private readonly EventBus _eventBus;
    
    [Inject]
    public BattlePipelineInstaller(
        DiContainer diContainer,
        BattlePipeline battlePipeline,
        EventBus eventBus
        )
    {
        _diContainer = diContainer;
        _battlePipeline = battlePipeline;
        _eventBus = eventBus;
    }

    public void Initialize()
    { 
        _battlePipeline.AddTask(new SetupServicesTask(_diContainer, _eventBus)); //Определить порядок хода, инициализировать сервисы типа HeroTracker
        _battlePipeline.AddTask(new SetupAbilityControllersTask(_diContainer, _eventBus)); //=> SetupObservers??? Пусть будут сервисы-обсерверы на каждый триггер
        _battlePipeline.AddTask(new StartBattleTask());
        _battlePipeline.AddTask(new RunRoundPipelineTask(_diContainer, _eventBus));
        _battlePipeline.AddTask(new FinishBattleTask());
    }
}