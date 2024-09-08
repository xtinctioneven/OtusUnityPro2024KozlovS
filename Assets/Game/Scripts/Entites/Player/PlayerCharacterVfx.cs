using System;
using UnityEngine;

[Serializable]
public class PlayerCharacterVfx
{
    [SerializeField] private ParticleSystem _takeDamageVfx;
    [SerializeField] private ParticleSystem _shootVfx;
    
    private PlayerCharacterCore _playerCharacterCore;

    public void Compose(PlayerCharacterCore playerCharacterCore)
    {
        _playerCharacterCore = playerCharacterCore;
    }

    public void OnEnable()
    {
        _playerCharacterCore.ShootComponent.ShootFiredEvent.Subscribe(OnShoot);
        _playerCharacterCore.LifeComponent.OnDamageTakenEvent.Subscribe(OnDamageTaken);
    }

    public void OnDisable()
    {
        _playerCharacterCore.ShootComponent.ShootFiredEvent.Unsubscribe(OnShoot);
        _playerCharacterCore.LifeComponent.OnDamageTakenEvent.Unsubscribe(OnDamageTaken);
    }

    private void OnDamageTaken()
    {
        _takeDamageVfx.Play();
    }

    private void OnShoot()
    {
        _shootVfx.Play();
    }
}