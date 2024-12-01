public struct EntityInteractionEvent: IEvent
{
    public EntityInteractionData EntityInteractionData;
    public EntityInteractionEvent(EntityInteractionData entityInteractionData)
    {
        EntityInteractionData = entityInteractionData;
    }
}