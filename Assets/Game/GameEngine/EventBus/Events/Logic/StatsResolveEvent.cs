using Game.Gameplay;

public struct StatsResolveEvent: IEvent
{
    // public IEntity Entity;
    public EntityInteractionData InteractionData;
    public StatsResolveEvent(//IEntity entity, 
        EntityInteractionData interactionData)
    {
        //Entity = entity;
        InteractionData = interactionData;
    }
}