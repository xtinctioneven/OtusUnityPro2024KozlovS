using Atomic.Elements;
using Zenject;

public class PlayerDeathObserver
{
    public AtomicEvent GameOver = new AtomicEvent();
    private AtomicVariable<bool> _isPlayerDead; 

    [Inject]
    private void Construct(PlayerCharacter playerCharacter)
    {
        if (playerCharacter.TryGet(LifeAPI.IS_DEAD_VALUE, out  _isPlayerDead))
        {
            _isPlayerDead.Subscribe(OnPlayerDeath);
        }
    }

    private void OnPlayerDeath(bool isPlayerDead)
    {
        if (isPlayerDead)
        {
            GameOver?.Invoke();
        }
    }
}