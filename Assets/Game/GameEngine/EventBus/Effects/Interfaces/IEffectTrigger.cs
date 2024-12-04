namespace Game.Gameplay
{
    public interface IEffectTrigger : IEffectTarget
    {
        public TriggerReason TriggerReason { get; }
    }
}