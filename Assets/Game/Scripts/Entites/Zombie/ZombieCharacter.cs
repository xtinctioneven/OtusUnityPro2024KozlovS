using System.Collections;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using Zenject;

public class ZombieCharacter : AtomicEntity
{
    [Get(MoveAPI.MOVE_DIRECTION)]
    public IAtomicVariable<Vector3> MoveDirection => _zombieCharacterCore.MoveComponent.MoveDirection;

    [Get(StrikeAPI.STRIKE_REQUEST)] 
    public IAtomicAction StrikeRequest => _zombieCharacterCore.StrikeComponent.StrikeRequest;

    [Get(LifeAPI.TAKE_DAMAGE_ACTION)] 
    public IAtomicAction<int> TakeDamage => _zombieCharacterCore.LifeComponent.TakeDamageAction;
    
    [Get(LifeAPI.IS_DEAD_VALUE)] 
    public AtomicVariable<bool> IsDead => _zombieCharacterCore.LifeComponent.IsDead;

    [SerializeField] private Transform _zombieVisual;
    [SerializeField] private ZombieCharacterCore _zombieCharacterCore;
    
    [SerializeField] private ZombieCharacterAnimation _zombieCharacterAnimation;
    [SerializeField] private ZombieCharacterVfx _zombieCharacterVfx;
    [SerializeField] private ZombieCharacterAudio _zombieCharacterAudio;

    private Transform _target;

    [Inject]
    private void Construct(PlayerCharacter playerCharacter)
    {
        _target = playerCharacter.transform;
    }
    
    private void Awake()
    {
        _zombieCharacterCore.Compose(_target);
        _zombieCharacterAnimation.Compose(_zombieCharacterCore);
        _zombieCharacterVfx.Compose(_zombieCharacterCore);
        _zombieCharacterAudio.Compose(_zombieCharacterCore);
        
        _zombieCharacterCore.LifeComponent.IsDead.Subscribe(isDead =>
        {
            if (isDead)
            {
                StartCoroutine(DeadSequence());
            }
        });
    }

    private void OnEnable()
    {
        _zombieCharacterAnimation.OnEnable();
        _zombieCharacterAudio.OnEnable();
        _zombieCharacterVfx.OnEnable();
    }

    private void OnDisable()
    {
        _zombieCharacterAnimation.OnDisable();
        _zombieCharacterAudio.OnDisable();
        _zombieCharacterVfx.OnDisable();
    }
    
    private void Update()
    {
        _zombieCharacterCore.Update(Time.deltaTime);
    }

    private IEnumerator DeadSequence()
    {
        _zombieVisual.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        OnDisable();
        Destroy(gameObject);
    }
}