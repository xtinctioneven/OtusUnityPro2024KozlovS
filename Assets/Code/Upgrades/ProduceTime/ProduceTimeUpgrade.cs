using Game.GamePlay.Conveyor;
using Game.GamePlay.Conveyor.Components;
using Zenject;

public class ProduceTimeUpgrade : Upgrade, IInitializable
{
    private ConveyorEntity _conveyor;
    private readonly ProduceTimeUpgradeConfig _config;
    
    public ProduceTimeUpgrade(ProduceTimeUpgradeConfig config) : base(config)
    {
        _config = config;
    }

    [Inject]
    public void Construct(ConveyorEntity conveyor)
    {
        _conveyor = conveyor;
    }

    protected override void OnUpgrade(int level)
    {
        SetLevel(level);
    }
    
    public void Initialize()
    {
        SetLevel(Level);
    }

    private void SetLevel(int level)
    {
        float produceTime = _config.UpgradeTable.GetProduceTime(level);
        _conveyor.Get<IConveyor_SetProduceTimeComponent>().SetProduceTime(produceTime);
    }
}