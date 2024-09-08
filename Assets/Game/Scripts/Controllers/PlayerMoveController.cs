using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using Zenject;

public class PlayerMoveController : ITickable
{
    private AtomicEntity _playerEntity;
    
    [Inject]
    public void Construct(PlayerCharacter playerCharacter)
    {
        _playerEntity = playerCharacter ;
    }

    public void Tick()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        var moveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.z += 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection.z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x -= 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x += 1;
        }
        Move(moveDirection.normalized);
    }

    private void Move(Vector3 moveDirection)
    {
        if (_playerEntity.TryGet(MoveAPI.MOVE_DIRECTION, out IAtomicVariable<Vector3> entityMoveDirection))
        {
            entityMoveDirection.Value = moveDirection;
        }
    }
}