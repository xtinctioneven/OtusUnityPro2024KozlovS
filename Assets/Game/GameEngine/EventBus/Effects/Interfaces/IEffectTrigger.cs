namespace Game.Gameplay
{
    public interface IEffectTrigger : IEffectTarget, IEffectUseCounts
    {
        bool IEffect.CanBeUsed => Enabled && CountsLeft > 0;
        public TriggerReason TriggerReason { get; }
    }
}