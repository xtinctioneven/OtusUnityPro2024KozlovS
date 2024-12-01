using System;

public class DeathComponent
{
    public event Action<bool> OnIsDeadChanged;
    public bool IsDead { get; private set; }

    public DeathComponent(bool isDead = false)
    {
        IsDead = isDead;
    }

    public void Die()
    {
        IsDead = true;
        OnIsDeadChanged?.Invoke(IsDead);
    }
}