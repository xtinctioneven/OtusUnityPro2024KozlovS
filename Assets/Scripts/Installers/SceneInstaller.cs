using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public class SceneInstaller : MonoInstaller
    {
        public const string WORLD_TRANSFORM = "[WORLD]";
        public const string ENEMY_POOL = "EnemyPool";
        public const string BULLET_POOL = "BulletPool";
        public const string CHARACTER_ID = "Character";

        [SerializeField] private GameObject _character;
        [SerializeField] private float _enemySpawnInterval = 1f;
        [SerializeField] private PoolSettings _enemyPoolSettings;
        [SerializeField] private PoolSettings _bulletPoolSettings;
        [SerializeField] private LevelBoundsSettings _levelBoundsSettings;
        [SerializeField] private LevelBackgroundSettings _levelBackgroundSettings;
        [SerializeField] private EnemyPositionsSettings _enemyPositionsSettings;

        [Header("Game Pause References")]
        [SerializeField] private Image _playButtonVisual;
        [SerializeField] private Button _pauseButton;

        public override void InstallBindings()
        {
            //Common
            Container.BindInterfacesAndSelfTo<GameManager>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePauseManager>().FromNew().AsSingle().WithArguments(_playButtonVisual, _pauseButton);
            //Level
            Container.Bind<LevelBounds>().FromNew().AsSingle().WithArguments(_levelBoundsSettings);
            Container.BindInterfacesAndSelfTo<LevelBackground>().FromNew().AsSingle().WithArguments(_levelBackgroundSettings);
            Container.Bind<Transform>().WithId(WORLD_TRANSFORM).FromInstance(GameObject.Find(WORLD_TRANSFORM).transform);
            //Bullets
            Container.Bind<BulletManager>().FromNew().AsSingle();
            Container.Bind<Pool>().WithId(BULLET_POOL).FromNew().AsCached().WithArguments(_bulletPoolSettings);
            Container.Bind<BulletSpawner>().FromNew().AsSingle(); 
            Container.BindInterfacesAndSelfTo<BulletTracker>().FromNew().AsSingle();
            //Enemies
            Container.Bind<EnemyManager>().FromNew().AsSingle();
            Container.Bind<Pool>().WithId(ENEMY_POOL).FromNew().AsCached().WithArguments(_enemyPoolSettings);
            Container.Bind<EnemyPositions>().FromNew().AsSingle().WithArguments(_enemyPositionsSettings);
            Container.BindInterfacesAndSelfTo<EnemySpawner>().FromNew().AsSingle().WithArguments(_enemySpawnInterval);
            //Character
            Container.Bind<GameObject>().WithId(CHARACTER_ID).FromInstance(_character);
        }
    }
}
