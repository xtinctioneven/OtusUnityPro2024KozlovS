using System;
using UnityEngine;

[Serializable]
public class ZombieCharacterAudio
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _strikeSfx;
    [SerializeField] private AudioClip _deathSfx;
    
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

    private void OnStrike(int _)
    {
        _audioSource.clip = _strikeSfx;
        _audioSource.Play();
    }

    private void OnDeath(bool isDead)
    {
        if (isDead)
        {
            _audioSource.clip = _deathSfx;
            _audioSource.Play();
        }
    }
}