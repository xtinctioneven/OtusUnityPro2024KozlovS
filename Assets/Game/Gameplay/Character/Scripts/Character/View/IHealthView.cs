namespace Game.Gameplay
{
    public interface IHealthView
    {
        void Setup(HealthComponent healthComponent);
        void UpdateCurrentHealth(int newHealth);
        void UpdateMaxHealth(int newMaxHealth);
        void UpdateView();
        void Disable();
        void Enable();
    }
}