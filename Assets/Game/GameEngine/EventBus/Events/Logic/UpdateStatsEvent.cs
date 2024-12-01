using Game.Gameplay;

public struct UpdateStatsEvent : IEvent
{
    public IEntity Entity;
    public int HPValue;

    public UpdateStatsEvent(IEntity entity, int hpValue)
    {
        Entity = entity;
        HPValue = hpValue;
    }
}