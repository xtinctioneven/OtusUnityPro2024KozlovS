using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _poolContainer;
        [SerializeField] private int _poolSize;
        [SerializeField] private EnemyPositions _enemyPositions;
        private Pool _enemyPool;

        private void Awake()
        {
            _enemyPool = new Pool(_poolContainer, _enemyPrefab, _poolSize);
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

