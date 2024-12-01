using Game.Gameplay;

public struct DeathEvent : IEvent
{
    public IEntity Entity;

    public DeathEvent(IEntity entity)
    {
        Entity = entity;
    }
}