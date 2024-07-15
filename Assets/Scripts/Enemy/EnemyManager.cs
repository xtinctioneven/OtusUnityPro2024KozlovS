using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyManager
    {
        private EnemyPositions _enemyPositions;
        private GameObject _targetCharacter;

        [Inject]
        private void Construct(EnemyPositions enemyPositions, [Inject(Id = SceneInstaller.CHARACTER_ID)]GameObject targetCharacter)
        {
            _enemyPositions = enemyPositions;
            _targetCharacter = targetCharacter;
        }

        public void EnemySpawnCallback(GameObject newEnemyGO)
        {
            Vector3 attackPosition = _enemyPositions.GetAttackPosition();
            Enemy newEnemy = newEnemyGO.GetComponent<Enemy>();
            newEnemy.EnemyAttackAgent.SetTarget(_targetCharacter);
            newEnemy.EnemyMoveAgent.SetDestination(attackPosition);
        }
    }

}