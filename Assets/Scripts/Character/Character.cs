using Zenject;

namespace ShootEmUp
{
    public class Character : Unit
    {
        private CharacterController _characterController;
        private CharacterDeathObserver _characterDeathObserver;

        public CharacterController CharacterController { get { return _characterController; } }
        public CharacterDeathObserver CharacterDeathObserver { get { return _characterDeathObserver; } }

        [Inject]
        private void Construct(CharacterController characterController,CharacterDeathObserver characterDeathObserver)
        {
            _characterController = characterController;
            _characterDeathObserver = characterDeathObserver;
        }
    }
}