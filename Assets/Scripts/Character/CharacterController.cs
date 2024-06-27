using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour, IGameStartListener, IGamePlayListener, IGameFinishListener, IGamePauseListener
    {
        [SerializeField] private GameObject _character;
        [SerializeField] private BulletManager _bulletManager;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private MoveComponent _moveComponent;

        private void Start()
        {
            IGameListener.Register(this);
        }

        public void OnGameStart()
        {
            _inputManager.OnHorizontalMoveButtonPressed += InputManager_OnHorizontalMoveButtonPressed;
            _inputManager.OnShootButtonPressed += InputManager_OnShootButtonPressed;
        }

        public void OnGameFinish()
        {
            _inputManager.OnHorizontalMoveButtonPressed -= InputManager_OnHorizontalMoveButtonPressed;
            _inputManager.OnShootButtonPressed -= InputManager_OnShootButtonPressed;
        }

        public void OnGamePause()
        {
            enabled = false;
        }

        public void OnGamePlay()
        {
            enabled = true;
        }

        private void InputManager_OnHorizontalMoveButtonPressed(float horizontalDirection)
        {
            Vector2 moveDirection = new Vector2(horizontalDirection, 0) * Time.fixedDeltaTime;
            _moveComponent.MoveInDirection(moveDirection);
        }

        private void InputManager_OnShootButtonPressed()
        {
            _bulletManager.Shoot(_character, Vector2.up);
        }
    }
}