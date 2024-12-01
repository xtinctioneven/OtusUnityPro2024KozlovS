namespace Game.Gameplay
{
    public interface IEffectLink : IEffect, IEffectTarget, IEffectUseCounts
    {
        LinkStatusType SeekLinkStatus { get; set; }
    }
}