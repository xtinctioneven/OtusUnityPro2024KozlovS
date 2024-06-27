using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace ShootEmUp
{
    public class EnemySpawner : MonoBehaviour, IGameUpdateListener
    {
        [Header("Spawner settings")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _poolContainer;
        [SerializeField] private int _poolSize;
        [SerializeField] private float _spawnInterval;
        [Header("Enemy data")]
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private EnemyManager _enemyManager;
        private Timer _spawnTimer;
        private Pool _enemyPool;

        private void Awake()
        {
            _enemyPool = new Pool(_poolContainer, _enemyPrefab, _poolSize);
            _spawnTimer = new Timer(_spawnInterval);
        }

        public void Start()
        {
            IGameListener.Register(this);
        }

        public void OnGameUpdate(float deltaTime)
        {
            if (_spawnTimer.Tick(deltaTime))
            {
                Vector3 spawnPosition = _enemyPositions.GetSpawnPosition();
                GameObject newEnemy = Spawn(_worldTransform, spawnPosition);
                if (newEnemy != null)
                {
                    HitPointsComponent enemyHitPointsComponent = newEnemy.GetComponent<HitPointsComponent>();
                    enemyHitPointsComponent.OnHpEmpty += EnemySpawner_OnEnemyDestroyed;
                    enemyHitPointsComponent.Reset();
                    _enemyManager.EnemySpawnCallback(newEnemy);
                }
            }
        }

        private void EnemySpawner_OnEnemyDestroyed(GameObject destroyedEnemy)
        {
            destroyedEnemy.GetComponent<HitPointsComponent>().OnHpEmpty -= EnemySpawner_OnEnemyDestroyed;
            _enemyPool.Unspawn(destroyedEnemy);
        }

        public GameObject Spawn(Transform newParent, Vector3 spawnPosition)
        {
            GameObject enemy = _enemyPool.Spawn(newParent, spawnPosition);
            return enemy;
        }

        public void Unspawn(GameObject enemy)
        {
            _enemyPool.Unspawn(enemy);
        }
    }
}

