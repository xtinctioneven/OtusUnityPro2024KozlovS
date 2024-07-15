using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemySpawner : IInitializable, IGameUpdateListener
    {
        private float _spawnInterval;
        private EnemyPositions _enemyPositions;
        private Transform _worldTransform;
        private EnemyManager _enemyManager;
        private Timer _spawnTimer;
        private Pool _enemyPool;

        [Inject]
        private void Construct(
            EnemyPositions enemyPositions,
            [Inject(Id = SceneInstaller.WORLD_TRANSFORM)] Transform worldTransform,
            EnemyManager enemyManager,
            [Inject(Id = SceneInstaller.ENEMY_POOL)] Pool enemyPool,
            float spawnInterval
            )
        {
            _enemyPositions = enemyPositions;
            _worldTransform = worldTransform;
            _enemyManager = enemyManager;
            _enemyPool = enemyPool;
            _spawnInterval = spawnInterval;
        }

        public void Initialize()
        {
            _spawnTimer = new Timer(_spawnInterval);
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
                    HitPointsComponent enemyHitPointsComponent = newEnemy.GetComponent<Enemy>().HitPointsComponent;
                    enemyHitPointsComponent.OnHpEmpty += EnemySpawner_OnEnemyDestroyed;
                    enemyHitPointsComponent.Reset();
                    _enemyManager.EnemySpawnCallback(newEnemy);
                }
            }
        }
        private void EnemySpawner_OnEnemyDestroyed(Unit destroyedEnemy)
        {
            destroyedEnemy.HitPointsComponent.OnHpEmpty -= EnemySpawner_OnEnemyDestroyed;
            Unspawn(destroyedEnemy.gameObject);
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

