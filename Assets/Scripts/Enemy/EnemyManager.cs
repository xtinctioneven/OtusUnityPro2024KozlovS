using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private GameObject _targetCharacter;
        private readonly List<GameObject> _activeEnemies = new();


        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                Vector3 spawnPosition = _enemyPositions.GetSpawnPosition();
                GameObject enemy = _enemySpawner.Spawn(_worldTransform, spawnPosition);
                if (enemy != null)
                {
                    Vector3 attackPosition = _enemyPositions.GetAttackPosition();
                    _activeEnemies.Add(enemy);
                    enemy.GetComponent<HitPointsComponent>().hpEmpty += EnemyManager_OnEnemyDestroyed;
                    enemy.GetComponent<EnemyAttackAgent>().OnAttack += EnemyManager_OnEnemyShoot;
                    enemy.GetComponent<EnemyAttackAgent>().SetTarget(_targetCharacter);
                    enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition);
                }
            }
        }

        private void EnemyManager_OnEnemyDestroyed(GameObject enemy)
        {
            _activeEnemies.Remove(enemy);
            enemy.GetComponent<HitPointsComponent>().hpEmpty -= this.EnemyManager_OnEnemyDestroyed;
            enemy.GetComponent<EnemyAttackAgent>().OnAttack -= this.EnemyManager_OnEnemyShoot;
            _enemySpawner.Unspawn(enemy);
        }

        private void EnemyManager_OnEnemyShoot(GameObject enemy, Vector2 direction)
        {
            _bulletSystem.Shoot(enemy, direction);
        }
    }
}