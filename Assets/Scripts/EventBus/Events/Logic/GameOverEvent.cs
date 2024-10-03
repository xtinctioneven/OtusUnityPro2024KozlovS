public struct GameOverEvent : IEvent
{
    public GameOverCheckTask.GameOverReason GameOverReason;

    public GameOverEvent(GameOverCheckTask.GameOverReason reason)
    {
        GameOverReason = reason;
    }
}