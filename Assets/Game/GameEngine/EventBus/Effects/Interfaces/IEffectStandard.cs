namespace Game.Gameplay
{
    public interface IEffectStandard :  IEffectTarget, IEffectUseCounts
    {
        bool IEffect.CanBeUsed => Enabled && CountsLeft > 0;
    }
}