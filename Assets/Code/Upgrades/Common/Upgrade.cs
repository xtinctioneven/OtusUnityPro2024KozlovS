using UnityEngine;

public abstract class Upgrade
{
    public string Id => _config.Id;
    public int Level => _level;
    public int MaxLevel => _config.MaxLevel;
    public bool IsMaxLevelUp => _level >= _config.MaxLevel;
    public int NextPrice => _config.PriceTable.GetPrice(_level + 1);
    
    private readonly UpgradeConfig _config;
    private int _level;

    protected Upgrade(UpgradeConfig config)
    {
        _config = config;
        _level = 1;
    }
    
    public void LevelUp()
    {
        _level++;
        OnUpgrade(_level);
        Debug.Log($"<color=green>Leveled up {this.Id} to level {_level}!</color>");
    }

    public void SetupLevel(int level)
    {
        _level = level;
        Debug.Log($"<color=green>Setup {this.Id} to level {_level}!</color>");
    }
    
    protected abstract void OnUpgrade(int level);
}