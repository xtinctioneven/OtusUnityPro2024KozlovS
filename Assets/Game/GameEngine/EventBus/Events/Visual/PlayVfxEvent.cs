using Game.Gameplay;

public struct PlayVfxEvent : IEvent
{
    public EntityInteractionData InteractionData;

    public PlayVfxEvent(
        EntityInteractionData entityInteractionData
    )
    {
        InteractionData = entityInteractionData;
    }
}