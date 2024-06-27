using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private BulletManager _bulletManager;
        [SerializeField] private GameObject _targetCharacter;
        [SerializeField] private LevelBounds _levelBounds;

        public void EnemySpawnCallback(GameObject newEnemy)
        {
            Vector3 attackPosition = _enemyPositions.GetAttackPosition();
            EnemyAttackAgent enemyAttackAgent = newEnemy.GetComponent<EnemyAttackAgent>();
            enemyAttackAgent.SetTarget(_targetCharacter);
            enemyAttackAgent.SetBulletManager(_bulletManager);
            newEnemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition);
            newEnemy.GetComponent<MoveComponent>().SetLevelBounds(_levelBounds);
        }
    }

}