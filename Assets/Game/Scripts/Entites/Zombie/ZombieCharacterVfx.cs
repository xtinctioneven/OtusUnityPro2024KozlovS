using System;
using UnityEngine;

[Serializable]
public class ZombieCharacterVfx
{
    [SerializeField] private ParticleSystem _deathVfx;
    [SerializeField] private ParticleSystem _strikeVfx;
    
    private ZombieCharacterCore _zombieCharacterCore;

    public void Compose(ZombieCharacterCore zombieCharacterCore)
    {
        _zombieCharacterCore = zombieCharacterCore;
    }

    public void OnEnable()
    {
        _zombieCharacterCore.StrikeComponent.StrikeConnectedEvent.Subscribe(OnStrike);
        _zombieCharacterCore.LifeComponent.IsDead.Subscribe(OnDeath);
    }

    public void OnDisable()
    {
        _zombieCharacterCore.StrikeComponent.StrikeConnectedEvent.Unsubscribe(OnStrike);
        _zombieCharacterCore.LifeComponent.IsDead.Unsubscribe(OnDeath);
    }

    private void OnDeath(bool isDead)
    {
        if (isDead)
        {
            _deathVfx.Play();
        }
    }

    private void OnStrike(int _)
    {
        _strikeVfx.Play();
    }
}