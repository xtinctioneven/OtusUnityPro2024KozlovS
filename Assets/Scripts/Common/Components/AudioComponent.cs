using System;
using UnityEngine;

[Serializable]
public class AudioComponent
{
    public AudioClip[] StartTurnSounds;
    public AudioClip[] LowHealthSounds;
    public AudioClip[] AbilityCastSounds;
    public AudioClip[] DeathSounds;

    public AudioComponent(AudioClip[] startTurnSounds = null, AudioClip[] lowHealthSounds = null, 
        AudioClip[] abilityCastSounds = null, AudioClip[] deathSounds = null)
    {
        StartTurnSounds = startTurnSounds;
        LowHealthSounds = lowHealthSounds;
        AbilityCastSounds = abilityCastSounds;
        DeathSounds = deathSounds;
    }

    public AudioClip GetStartTurnSound()
    {
        return StartTurnSounds.Length == 0 ? null 
            : StartTurnSounds[UnityEngine.Random.Range(0, StartTurnSounds.Length)];
    }

    public AudioClip GetLowHealthSound()
    {
        return LowHealthSounds.Length == 0 ? null 
            : LowHealthSounds[UnityEngine.Random.Range(0, LowHealthSounds.Length)];
    }

    public AudioClip GetAbilityCast()
    {
        return AbilityCastSounds.Length == 0 ? null 
            : AbilityCastSounds[UnityEngine.Random.Range(0, AbilityCastSounds.Length)];
    }

    public AudioClip GetDeathSound()
    {
        return DeathSounds.Length == 0 ? null 
            : DeathSounds[UnityEngine.Random.Range(0, DeathSounds.Length)];
    }
}