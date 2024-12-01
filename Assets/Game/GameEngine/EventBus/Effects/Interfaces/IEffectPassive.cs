namespace Game.Gameplay
{
    public interface IEffectPassive : IEffect, IEffectUseCounts
    {
        bool IEffect.CanBeUsed => Enabled && CountsLeft > 0;
    }
}