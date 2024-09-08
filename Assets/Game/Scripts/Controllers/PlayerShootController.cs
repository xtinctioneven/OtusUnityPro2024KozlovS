using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using Zenject;

public class PlayerShootController : IInitializable, ITickable
{
    private AtomicEntity _playerEntity;
    private IAtomicAction _shootRequest;
    
    [Inject]
    public void Construct(PlayerCharacter playerCharacter)
    {
        _playerEntity = playerCharacter ;
    }

    public void Initialize()
    {
        _shootRequest = _playerEntity.Get<IAtomicAction>(ShootAPI.SHOOT_REQUEST);
    }

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _shootRequest?.Invoke();
        }
    }
}