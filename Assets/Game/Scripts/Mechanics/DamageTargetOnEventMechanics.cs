using Atomic.Elements;
using Atomic.Objects;

public class DamageTargetOnEventMechanics
{
    private readonly IAtomicEntity _targetEntity;

    public DamageTargetOnEventMechanics(
        IAtomicEvent<int> triggerEvent,
        IAtomicEntity targetEntity
    )
    {
        _targetEntity = targetEntity;
        triggerEvent.Subscribe(TryDoDamage);
    }

    private void TryDoDamage(int damage)
    {
        if (_targetEntity.TryGet<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE_ACTION, out var damageAction))
        {
            damageAction.Invoke(damage);
        }
    }
}