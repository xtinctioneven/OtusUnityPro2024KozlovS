using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterController : IGamePlayListener, IGameFinishListener, IGamePauseListener, IInitializable
    {
        private GameObject _character;
        private BulletManager _bulletManager;
        private InputManager _inputManager;
        private MoveComponent _moveComponent;

        [Inject]
        private void Construct(
            [Inject(Id = SceneInstaller.CHARACTER_ID)] GameObject character,
            BulletManager bulletManager,
            InputManager inputManager,
            MoveComponent moveComponent
            )
        {
            _character = character;
            _bulletManager = bulletManager;
            _inputManager = inputManager;
            _moveComponent = moveComponent;
        }

        public void Initialize()
        {
            IGameListener.Register(this);
        }

        public void OnGameFinish()
        {
            Unscribe();
        }

        public void OnGamePause()
        {
            Unscribe();
        }

        public void OnGamePlay()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            _inputManager.OnHorizontalMoveButtonPressed += InputManager_OnHorizontalMoveButtonPressed;
            _inputManager.OnShootButtonPressed += InputManager_OnShootButtonPressed;
        }

        private void Unscribe()
        {
            _inputManager.OnHorizontalMoveButtonPressed += InputManager_OnHorizontalMoveButtonPressed;
            _inputManager.OnShootButtonPressed += InputManager_OnShootButtonPressed;
        }

        private void InputManager_OnHorizontalMoveButtonPressed(float horizontalDirection)
        {
            Vector2 moveDirection = new Vector2(horizontalDirection, 0) * Time.fixedDeltaTime;
            _moveComponent.MoveInDirection(moveDirection);
        }

        private void InputManager_OnShootButtonPressed()
        {
            _bulletManager.Shoot(_character.GetComponent<Unit>(), Vector2.up);
        }
    }
}