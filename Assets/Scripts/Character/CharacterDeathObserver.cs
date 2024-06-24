using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] HitPointsComponent _characterHitPoints;
        [SerializeField] GameManager _gameManager;

        private void OnEnable()
        {
            _characterHitPoints.OnHpEmpty += CharacterDeathObserver_OnDeath;
        }

        private void OnDisable()
        {
            _characterHitPoints.OnHpEmpty -= CharacterDeathObserver_OnDeath;
        }

        private void CharacterDeathObserver_OnDeath(GameObject obj)
        {
            _gameManager.FinishGame();
        }
    }
}

