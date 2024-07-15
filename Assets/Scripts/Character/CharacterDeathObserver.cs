using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterDeathObserver : IGameStartListener, IGameFinishListener, IInitializable
    {
        HitPointsComponent _characterHitPoints;
        GameManager _gameManager;

        [Inject]
        private void Construct(HitPointsComponent hitPointsComponent, GameManager gameManager)
        {
            _characterHitPoints = hitPointsComponent;
            _gameManager = gameManager;
        }

        public void Initialize()
        {
            IGameListener.Register(this);
        }

        public void OnGameStart()
        {
            _characterHitPoints.OnHpEmpty += CharacterDeathObserver_OnDeath;
        }

        public void OnGameFinish()
        {
            _characterHitPoints.OnHpEmpty -= CharacterDeathObserver_OnDeath;
        }

        private void CharacterDeathObserver_OnDeath(Unit obj)
        {
            _gameManager.FinishGame();
        }
    }
}

