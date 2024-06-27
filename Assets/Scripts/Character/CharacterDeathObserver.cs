using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDeathObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] HitPointsComponent _characterHitPoints;
        [SerializeField] GameManager _gameManager;

        private void Start()
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

        private void CharacterDeathObserver_OnDeath(GameObject obj)
        {
            _gameManager.FinishGame();
        }
    }
}

