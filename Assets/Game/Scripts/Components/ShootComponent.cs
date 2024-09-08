using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;
[Serializable]
public class ShootComponent
{
    public AtomicEvent ShootRequest;
    public AtomicEvent ShootTryAction;
    public AtomicEvent ShootFiredEvent;
    public AtomicFunction<bool> CanFire;
    public AtomicVariable<int> AmmoSpendOnShoot;

    [SerializeField] private float _cooldownTime = 2f;
    [SerializeField] private bool _isOnCooldown;
    [SerializeField] private AtomicEntity _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    
    [ShowInInspector, ReadOnly] private float _cooldownTimer;

    private CompositeCondition _compositeCondition = new();

    public void Compose()
    {
        ShootTryAction?.Subscribe(Shoot);
        CanFire.Compose(() => !_isOnCooldown && _compositeCondition.IsTrue());
    }

    public void Update(float deltaTime)
    {
        if (_isOnCooldown)
        {
            _cooldownTimer -= deltaTime;
            if (_cooldownTimer <= 0)
            {
                _isOnCooldown = false;
            }
        }
    }

    public void Shoot()
    {
        if (!CanFire.Value)
        {
            return;
        }
        
        AtomicEntity bullet = GameObject.Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);

        if (bullet.TryGetVariable<Vector3>(MoveAPI.MOVE_DIRECTION, out IAtomicVariable<Vector3> moveDirection))
        {
            Vector3 bulletDirection = _firePoint.forward;
            bulletDirection.y = 0;
            moveDirection.Value = bulletDirection;
        }

        _cooldownTimer = _cooldownTime;
        _isOnCooldown = true;
        
        ShootFiredEvent.Invoke();
    }

    public void AppendCondtion(Func<bool> condition)
    {
        _compositeCondition.Append(condition);
    }
}