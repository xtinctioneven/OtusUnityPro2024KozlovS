using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(
    fileName = "HeroConfig",
    menuName = "Battler Configs/New Hero Config"
)]
public class HeroConfig : ScriptableObject
{
    public string HeroName;
    public int HealthValue;
    public int AttackValue;
    public AbilityConfig Ability;
    public AudioClip[] TurnStartSounds;
    public AudioClip[] LowHealthSounds;
    public AudioClip[] AbilityCastSounds;
    public AudioClip[] DeathSounds;
}