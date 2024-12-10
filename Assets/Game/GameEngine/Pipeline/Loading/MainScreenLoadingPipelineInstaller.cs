using Zenject;

public class MainScreenLoadingPipelineInstaller: IInitializable
{
    private readonly DiContainer _diContainer;
    private readonly LoadingPipeline _loadingPipeline;
    private readonly EventBus _eventBus;
    
    [Inject]
    public MainScreenLoadingPipelineInstaller(
        DiContainer diContainer,
        LoadingPipeline loadingPipeline,
        EventBus eventBus
    )
    {
        _diContainer = diContainer;
        _loadingPipeline = loadingPipeline;
        _eventBus = eventBus;
    }

    public void Initialize()
    { 
        // _loadingPipeline.AddTask(new (SpawnCharactersVisuals));
        
        // _roundPipeline.AddTask(new RefreshServicesTask());
        // _loadingPipeline.AddTask(new StartRoundTask(_diContainer, _eventBus));
        // _loadingPipeline.AddTask(new RunTurnPipelineTask(_diContainer, _eventBus));
        // _loadingPipeline.AddTask(new FinishRoundTask(_diContainer, _eventBus));
    }
}