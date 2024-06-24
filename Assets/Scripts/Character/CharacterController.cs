using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        [SerializeField] private BulletManager _bulletManager;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private MoveComponent _moveComponent;

        private void OnEnable()
        {
            _inputManager.OnHorizontalMoveButtonPressed += InputManager_OnHorizontalMoveButtonPressed;
            _inputManager.OnShootButtonPressed += InputManager_OnShootButtonPressed;
        }

        private void OnDisable()
        {
            _inputManager.OnHorizontalMoveButtonPressed -= InputManager_OnHorizontalMoveButtonPressed;
            _inputManager.OnShootButtonPressed -= InputManager_OnShootButtonPressed;
        }

        private void InputManager_OnHorizontalMoveButtonPressed(float horizontalDirection)
        {
            Vector2 moveDirection = new Vector2(horizontalDirection, 0);
            _moveComponent.MoveByRigidbodyVelocity(moveDirection * Time.fixedDeltaTime);
        }

        private void InputManager_OnShootButtonPressed()
        {
            _bulletManager.Shoot(_character, Vector2.up);
        }
    }
}