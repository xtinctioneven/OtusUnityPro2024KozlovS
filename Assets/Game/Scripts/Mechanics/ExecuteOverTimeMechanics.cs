using Atomic.Elements;

public class ExecuteOverTimeMechanics
{
    private readonly IAtomicAction _action;
    private readonly float _executeInterval;
    private readonly IAtomicValue<bool> _canExecute;
    private float _executeTimer;

    public ExecuteOverTimeMechanics(
        IAtomicAction action,
        float executeInterval,
        IAtomicValue<bool> canExecute
        )
    {
        _action = action;
        _executeInterval = executeInterval;
        _canExecute = canExecute;
        _executeTimer = _executeInterval;
    }

    public void Update(float deltaTime)
    {
        if (!_canExecute.Value)
        {
            return;
        }
        
        _executeTimer -= deltaTime;
        if (_executeTimer <= 0)
        {
            _action?.Invoke();
            _executeTimer = _executeInterval;
        }
    }
}