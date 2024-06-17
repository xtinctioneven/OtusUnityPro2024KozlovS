using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private InputManager _inputManager;
        private MoveComponent _moveComponent;
        private WeaponComponent _weaponComponent;

        private void Start()
        {
            _character.TryGetComponent<MoveComponent>(out _moveComponent);
            _character.TryGetComponent<WeaponComponent>(out _weaponComponent);
            _inputManager.OnHorizontalMoveButtonPressed += InputManager_OnHorizontalMoveButtonPressed;
            _inputManager.OnShootButtonPressed += InputManager_OnShootButtonPressed;
        }

        private void InputManager_OnHorizontalMoveButtonPressed(float horizontalDirection)
        {
            Vector2 moveDirection = new Vector2(horizontalDirection, 0);
            _moveComponent.MoveByRigidbodyVelocity(moveDirection * Time.fixedDeltaTime);
        }

        private void InputManager_OnShootButtonPressed()
        {
            if (_weaponComponent != null)
            {
                _bulletSystem.Shoot(_character);
            }
        }
    }
}