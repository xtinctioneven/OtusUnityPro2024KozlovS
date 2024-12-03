namespace Game.Gameplay
{
    public interface IEffectUseCounts : IEffect
    {
        int CountsLeft { get; }
        int InitialUseCounts { get; }
        int MaxUseCounts { get; }
        int CountsUsed { get; }
        public void AddCount(int addCount);
        public int SubtractCount(int subtractCount = 1);
        public bool TryUseCount(int count = 1);
        public void ResetCounts();
    }
}