using System;
using Atomic.Elements;
using Atomic.Objects;
using Zenject;

public class PlayerStatsPresenter : IInitializable
{
    public Action<string> OnHitPointsChanged;
    public Action<string> OnBulletsChanged;
    public Action<string> OnKillsChanged;
    private PlayerCharacter _playerCharacter;
    private EnemySystem _enemySystem;
    private AtomicVariable<int> _playerHitPoints;
    private AtomicVariable<int> _currentAmmo;
    private AtomicVariable<int> _maxAmmo;
    private int _killsCount = 0;
    
    [Inject]
    private void Construct(PlayerCharacter playerCharacter, EnemySystem enemySystem)
    {
        _playerCharacter = playerCharacter;
        _enemySystem = enemySystem;
    }

    public void Initialize()
    {
        if (_playerCharacter.TryGet(LifeAPI.HIT_POINTS_VALUE, out _playerHitPoints))
        {
            _playerHitPoints.Subscribe(UpdateHitPointsText);
        }
        if (_playerCharacter.TryGet(AmmoAPI.MAX_AMMO_VALUE, out _maxAmmo))
        {
            //
        }
        if (_playerCharacter.TryGet(AmmoAPI.CURRENT_AMMO_VALUE, out _currentAmmo))
        {
            _currentAmmo.Subscribe(UpdateBulletsText);
        }
        _enemySystem.ZombieTrackerComponent.OnEntityTrack.Subscribe(ZombieDeathSubscription);
        _enemySystem.ZombieTrackerComponent.OnEntityUntrack.Unsubscribe(ZombieDeathSubscription);
        
        UpdateHitPointsText(_playerHitPoints.Value);
        UpdateBulletsText(_maxAmmo.Value);
        UpdateKillsText(0);
    }

    private void UpdateHitPointsText(int currentHitPoints)
    {
        OnHitPointsChanged?.Invoke($"HIT POINT: {currentHitPoints}");
    }

    private void UpdateBulletsText(int currentAmmo)
    {
        OnBulletsChanged?.Invoke($"BULLETS: {currentAmmo}/{_maxAmmo.Value}");
    }

    private void ZombieDeathSubscription(AtomicEntity entity)
    {
        (entity as ZombieCharacter).IsDead.Subscribe(isDead =>
        {
            if (isDead)
            {
                UpdateKillsText();
            }
        });
    }

    private void UpdateKillsText(int increment = 1)
    {
        _killsCount+= increment;
        OnKillsChanged?.Invoke($"KILLS: {_killsCount}");
    }
}