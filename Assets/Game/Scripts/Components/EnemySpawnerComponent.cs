using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using Zenject;

[Serializable]
public class EnemySpawnerComponent<T> where T : AtomicEntity
{
    public AtomicAction SpawnEnemy;
    public AtomicEvent<T> OnEnemySpawn;
    
    [SerializeField] private AtomicEntity _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _enemyContainer;
    private DiContainer _diContainer;

    public void Compose(DiContainer diContainer)
    {
        _diContainer = diContainer;
        SpawnEnemy.Compose(Spawn);
    }

    private void Spawn()
    {
        var spawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
        var newEntity = _diContainer.InstantiatePrefab(_enemyPrefab, spawnPoint.position, Quaternion.identity, _enemyContainer).GetComponent<T>();
        OnEnemySpawn?.Invoke(newEntity);
    }
}