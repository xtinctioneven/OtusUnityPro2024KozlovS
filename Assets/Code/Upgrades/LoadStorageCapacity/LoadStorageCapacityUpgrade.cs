using Game.GamePlay.Conveyor;
using Game.GamePlay.Conveyor.Components;
using Zenject;

public class LoadStorageCapacityUpgrade : Upgrade, IInitializable
{
    private ConveyorEntity _conveyor;
    private readonly LoadStorageCapacityUpgradeConfig _config;
    
    public LoadStorageCapacityUpgrade(LoadStorageCapacityUpgradeConfig config) : base(config)
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
        int loadStorageCapacity = _config.UpgradeTable.GetLoadStorageCapacity(level);
        _conveyor.Get<IConveyor_SetLoadStorageComponent>().SetLoadStorage(loadStorageCapacity);
    }
}