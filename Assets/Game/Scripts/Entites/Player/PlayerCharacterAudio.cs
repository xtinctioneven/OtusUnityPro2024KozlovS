using System;
using UnityEngine;

[Serializable]
public class PlayerCharacterAudio
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _shootSfx;
    [SerializeField] private AudioClip _takeDamageSfx;
    [SerializeField] private AudioClip _deathSfx;
    
    private PlayerCharacterCore _playerCharacterCore;

    public void Compose(PlayerCharacterCore playerCharacterCore)
    {
        _playerCharacterCore = playerCharacterCore;
    }

    public void OnEnable()
    {
        _playerCharacterCore.ShootComponent.ShootFiredEvent.Subscribe(OnShoot);
        _playerCharacterCore.LifeComponent.OnDamageTakenEvent.Subscribe(OnDamageTaken);
        _playerCharacterCore.LifeComponent.IsDead.Subscribe(OnDeath);
    }

    public void OnDisable()
    {
        _playerCharacterCore.ShootComponent.ShootFiredEvent.Unsubscribe(OnShoot);
        _playerCharacterCore.LifeComponent.OnDamageTakenEvent.Unsubscribe(OnDamageTaken);
        _playerCharacterCore.LifeComponent.IsDead.Unsubscribe(OnDeath);
    }

    private void OnDamageTaken()
    {
        _audioSource.volume = 1f;
        _audioSource.clip = _takeDamageSfx;
        _audioSource.Play();
    }

    private void OnShoot()
    {
        _audioSource.volume = 0.2f;
        _audioSource.clip = _shootSfx;
        _audioSource.Play();
    }

    private void OnDeath(bool isDead)
    {
        if (isDead)
        {
            _audioSource.volume = 1f;
            _audioSource.clip = _deathSfx;
            _audioSource.Play();
        }
    }
}