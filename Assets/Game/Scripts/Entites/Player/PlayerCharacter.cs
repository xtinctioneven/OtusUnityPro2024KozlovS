using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public class PlayerCharacter : AtomicEntity
{
    [Get(MoveAPI.MOVE_DIRECTION)]
    public IAtomicVariable<Vector3> MoveDirection => _playerCharacterCore.MoveComponent.MoveDirection;

    [Get(ShootAPI.SHOOT_REQUEST)] 
    public IAtomicAction ShootRequest => _playerCharacterCore.ShootComponent.ShootRequest;
    
    [Get(AmmoAPI.CURRENT_AMMO_VALUE)] 
    public IAtomicVariable<int> CurrentAmmo => _playerCharacterCore.AmmoComponent.CurrentAmmo;
    [Get(AmmoAPI.MAX_AMMO_VALUE)] 
    public IAtomicVariable<int> MaxAmmo => _playerCharacterCore.AmmoComponent.MaxAmmo;
    
    [Get(LifeAPI.TAKE_DAMAGE_ACTION)] 
    public IAtomicAction<int> TakeDamage => _playerCharacterCore.LifeComponent.TakeDamageAction;
    [Get(LifeAPI.HIT_POINTS_VALUE)] 
    public IAtomicVariable<int> HitPoints => _playerCharacterCore.LifeComponent.HitPoints;
    [Get(LifeAPI.IS_DEAD_VALUE)] 
    public IAtomicVariable<bool> IsDead => _playerCharacterCore.LifeComponent.IsDead;
    
    [SerializeField] private PlayerCharacterCore _playerCharacterCore;
    [SerializeField] private PlayerCharacterAnimation _playerCharacterAnimation;
    [SerializeField] private PlayerCharacterVfx _playerCharacterVfx;
    [SerializeField] private PlayerCharacterAudio _playerCharacterAudio;

    private void Awake()
    {
        _playerCharacterCore.Compose();
        _playerCharacterAnimation.Compose(_playerCharacterCore);
        _playerCharacterAudio.Compose(_playerCharacterCore);
        _playerCharacterVfx.Compose(_playerCharacterCore);
    }

    private void OnEnable()
    {
        _playerCharacterAnimation.OnEnable();
        _playerCharacterVfx.OnEnable();
        _playerCharacterAudio.OnEnable();
    }

    private void OnDisable()
    {
        _playerCharacterAnimation.OnDisable();
        _playerCharacterVfx.OnDisable();
        _playerCharacterAudio.OnDisable();
    }

    private void Update()
    {
        _playerCharacterCore.Update(Time.deltaTime);
    }
}