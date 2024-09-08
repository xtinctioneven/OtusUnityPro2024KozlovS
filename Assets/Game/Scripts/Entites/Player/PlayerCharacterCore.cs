using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

[Serializable]
public class PlayerCharacterCore
{
    [SerializeField] private Collider _collider;
    public MoveComponent MoveComponent;
    public LifeComponent LifeComponent;
    [ShowInInspector, ReadOnly] private Vector3 _lookTargetPoint;
    public RotateComponent RotateComponent;
    public ShootComponent ShootComponent;
    [SerializeField] private float _ammoReloadTime = 2f;
    public AmmoComponent AmmoComponent;
    
    private LookAtTargetMechanics _lookAtTargetMechanics;
    private ExecuteOverTimeMechanics _ammoReloadOverTimeMechanics;

    [Inject]
    public void Compose()
    {
        LifeComponent.Compose();
        MoveComponent.Compose();
        MoveComponent.AppendCondition(LifeComponent.IsAlive);
        RotateComponent.Compose();
        RotateComponent.AppendCondition(LifeComponent.IsAlive);
        ShootComponent.Compose();
        ShootComponent.AppendCondtion(LifeComponent.IsAlive);
        AmmoComponent.Compose();
        ShootComponent.AppendCondtion(() => AmmoComponent.HaveAmmo());

        var lookAtTargetPosition = new AtomicFunction<Vector3>(() =>
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _lookTargetPoint = hit.point;
                _lookTargetPoint.y = 0;
            }
            return _lookTargetPoint;
        });

        var rotationRootPosition = new AtomicFunction<Vector3>(() => RotateComponent.RotationRoot.position);

        var canReload = new AtomicFunction<bool>(() =>
        {
            return (!AmmoComponent.IsFullAmmo());
        });

        _lookAtTargetMechanics = new LookAtTargetMechanics(RotateComponent.RotateAction, lookAtTargetPosition,
            rotationRootPosition);
        _ammoReloadOverTimeMechanics = new ExecuteOverTimeMechanics(AmmoComponent.ReloadAction,
            _ammoReloadTime, canReload);
        
        ShootComponent.ShootFiredEvent.Subscribe(() =>
        {
            AmmoComponent.SpendAmmoAction.Invoke(ShootComponent.AmmoSpendOnShoot.Value);
        });
        
        LifeComponent.IsDead.Subscribe(isDead => _collider.enabled = !isDead);
    }

    public void Update(float deltaTime)
    {
        MoveComponent.Update(deltaTime);
        ShootComponent.Update(deltaTime);
        
        _lookAtTargetMechanics.Update();
        _ammoReloadOverTimeMechanics.Update(deltaTime);
    }
}