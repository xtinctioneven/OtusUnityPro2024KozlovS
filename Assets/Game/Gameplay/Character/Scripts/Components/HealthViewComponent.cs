using System;
using Game.Gameplay;

[Serializable]
public class HealthViewComponent
{
    private IHealthView _value;

    public HealthViewComponent(IHealthView healthView, HealthComponent healthComponent)
    {
        _value = healthView;
        _value.Setup(healthComponent);
    }

    public void UpdateCurrentHealth(int currentHealth)
    {
        _value.UpdateCurrentHealth(currentHealth);
    }

    public void UpdateMaxHealth(int maxHealth)
    {
        _value.UpdateMaxHealth(maxHealth);
    }

    public void DisableView()
    {
        _value.Disable();
    }

    public void EnableView()
    {
        _value.Enable();
    }
}