using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using Zenject;

public class EnemySystem : AtomicEntity
{
    public EnemySpawnerComponent<ZombieCharacter> ZombieSpawnerComponent;
    public EntitiesTrackerComponent ZombieTrackerComponent;
    [SerializeField] private float _zombieSpawnInterval;
    private ExecuteOverTimeMechanics _zombieSpawnOverTimeMechanics;
    private DiContainer _diContainer;
    private PlayerDeathObserver _playerDeathObserver;
    
    [Inject]
    public void Construct(DiContainer diContainer, PlayerDeathObserver playerDeathObserver)
    {
        _diContainer = diContainer;
        _playerDeathObserver = playerDeathObserver;
    }
    
    private void Awake()
    {
        ZombieSpawnerComponent.Compose(_diContainer);
        ZombieTrackerComponent.Compose();
        ZombieTrackerComponent.TrackType(typeof(ZombieCharacter));
        ZombieSpawnerComponent.OnEnemySpawn.Subscribe(ZombieTrackerComponent.Track);
        
        ZombieSpawnerComponent.OnEnemySpawn.Subscribe(zombie =>
        {
            zombie.IsDead.Subscribe(isDead =>
            {
                if (isDead)
                {
                    ZombieTrackerComponent.Untrack(zombie);
                }
            });
        });
        
        _playerDeathObserver.GameOver.Subscribe(() =>
        {
            foreach (var zombie in ZombieTrackerComponent.TrackedEntities)
            {
                zombie.enabled = false;
            }
        });

        var canSpawn = new AtomicVariable<bool>(true);
        _playerDeathObserver.GameOver.Subscribe(()=>
        {
            canSpawn.Value = false;
        });
        
        _zombieSpawnOverTimeMechanics = new ExecuteOverTimeMechanics(ZombieSpawnerComponent.SpawnEnemy, 
            _zombieSpawnInterval, canSpawn);
    }

    private void Update()
    {
        _zombieSpawnOverTimeMechanics.Update(Time.deltaTime);
    }
}