public class TurnPipeline : Pipeline
{
    public void SkipHeroTurn()
    {
        if (!TryToSkipToTask(typeof(PlayerTurnEndTask)))
        {
            throw new System.Exception("Cannot skip hero turn!");
        };
    }
}