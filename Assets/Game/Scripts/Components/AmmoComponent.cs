using System;
using Atomic.Elements;

[Serializable]
public class AmmoComponent
{
    public AtomicEvent ReloadAction;
    public AtomicEvent<int> SpendAmmoAction;
    public AtomicVariable<int> CurrentAmmo;
    public AtomicVariable<int> MaxAmmo;
    public AtomicVariable<int> AmmoOnReload;

    public void Compose()
    {
        ReloadAction.Subscribe(Reload);
        SpendAmmoAction.Subscribe(SpendAmmo);
        CurrentAmmo.Value = MaxAmmo.Value;
    }

    public bool HaveAmmo(int amount = 1)
    {
        return CurrentAmmo.Value >= amount;
    }

    public bool IsFullAmmo()
    {
        return CurrentAmmo.Value == MaxAmmo.Value;
    }

    private void Reload()
    {
        if (IsFullAmmo())
        {
            return;
        }
        CurrentAmmo.Value = Math.Min(CurrentAmmo.Value + AmmoOnReload.Value, MaxAmmo.Value);
    }

    private void SpendAmmo(int ammoSpend = 1)
    {
        if (!HaveAmmo())
        {
            return;
        }
        CurrentAmmo.Value -= ammoSpend;
    }
}