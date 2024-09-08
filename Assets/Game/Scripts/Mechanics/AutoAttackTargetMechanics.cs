using Atomic.Elements;

public class AutoAttackTargetMechanics
{
    private IAtomicAction _attackAction;
    private IAtomicValue<bool> _canAttack;

    public AutoAttackTargetMechanics(
        IAtomicAction attackAction,
        IAtomicValue<bool> canAttack
    )
    {
        _attackAction = attackAction;
        _canAttack = canAttack;
    }

    public void Update()
    {
        if (_canAttack.Value)
        {
            _attackAction.Invoke();
        }
    }
}