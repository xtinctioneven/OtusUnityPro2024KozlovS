public interface IEquippableComponent
{
    void Apply(IHero hero);
    void Discard(IHero hero);
}