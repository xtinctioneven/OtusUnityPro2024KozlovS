using Game.GamePlay.Conveyor;
using Game.GamePlay.Conveyor.Components;
using Zenject;

public class UnloadStorageCapacityUpgrade : Upgrade, IInitializable
{
    private ConveyorEntity _conveyor;
    private readonly UnloadStorageCapacityUpgradeConfig _config;
    
    public UnloadStorageCapacityUpgrade(UnloadStorageCapacityUpgradeConfig config) : base(config)
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
        int unloadStorageCapacity = _config.UpgradeTable.GetUnloadStorageCapacity(level);
        _conveyor.Get<IConveyor_SetUnloadStorageComponent>().SetUnloadStorage(unloadStorageCapacity);
    }
}