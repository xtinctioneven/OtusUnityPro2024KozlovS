public interface IEffectableComponent
{
    void Apply(IHero hero);
    void Discard(IHero hero);
}