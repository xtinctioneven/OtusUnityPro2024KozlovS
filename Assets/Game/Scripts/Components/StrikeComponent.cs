using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class StrikeComponent
{
    public AtomicEvent StrikeRequest;
    public AtomicEvent StrikeStartAction;
    public AtomicEvent<int> StrikeConnectedEvent;

    public AtomicFunction<bool> CanStrike;
    public readonly AtomicValue<int> Damage = new AtomicValue<int>(1);
    [SerializeField] private float _cooldownTime = 2f;
    [SerializeField] private bool _isOnCooldown;
    
    [ShowInInspector, ReadOnly] private float _cooldownTimer;

    private CompositeCondition _compositeCondition = new();

    public void Compose()
    {
        StrikeStartAction?.Subscribe(Strike);
        CanStrike.Compose(() => !_isOnCooldown && _compositeCondition.IsTrue());
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

    public void Strike()
    {
        if (!CanStrike.Value)
        {
            return;
        }

        _cooldownTimer = _cooldownTime;
        _isOnCooldown = true;
        
        StrikeConnectedEvent.Invoke(Damage.Value);
    }

    public void AppendCondtion(Func<bool> condition)
    {
        _compositeCondition.Append(condition);
    }
}