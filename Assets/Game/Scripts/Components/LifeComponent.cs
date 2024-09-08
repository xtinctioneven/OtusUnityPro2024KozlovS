using System;
using Atomic.Elements;

[Serializable]
public class LifeComponent
{
    public AtomicEvent<int> TakeDamageAction;
    public AtomicEvent OnDamageTakenEvent;
    public AtomicVariable<bool> IsDead;
    public AtomicVariable<int> HitPoints;

    public void Compose()
    {
        TakeDamageAction.Subscribe(TakeDamage);
    }

    public bool IsAlive()
    {
        return !IsDead.Value;
    }

    private void TakeDamage(int damageAmount)
    {
        if (IsDead.Value)
        {
            return;
        }
        
        HitPoints.Value -= damageAmount;
        if (HitPoints.Value <= 0)
        {
            IsDead.Value = true;
        }
        else
        {
            OnDamageTakenEvent?.Invoke();
        }
    }
}