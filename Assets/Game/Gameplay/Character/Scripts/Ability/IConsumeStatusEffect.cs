namespace Game.Gameplay
{
    public interface IConsumeStatusEffect : IStatusEffect
    {
        // public string Name { get;}
        public StatusEffectType StatusEffectType { get;}
    }
}