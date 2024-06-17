using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] GameObject _character;
        [SerializeField] GameManager _gameManager;

        private void OnEnable()
        {
            _character.GetComponent<HitPointsComponent>().hpEmpty += CharacterDeathObserver_OnDeath;
        }

        private void OnDisable()
        {
            _character.GetComponent<HitPointsComponent>().hpEmpty -= CharacterDeathObserver_OnDeath;
        }

        private void CharacterDeathObserver_OnDeath(GameObject obj)
        {
            _gameManager.FinishGame();
        }
    }
}

