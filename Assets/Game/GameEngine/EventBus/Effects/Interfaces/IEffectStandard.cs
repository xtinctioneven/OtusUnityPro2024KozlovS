namespace Game.Gameplay
{
    public interface IEffectStandard : IEffect, IEffectTarget, IEffectUseCounts
    {
        bool IEffect.CanBeUsed => Enabled && CountsLeft > 0;
    }
}