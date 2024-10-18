using Sirenix.OdinInspector;
using UnityEngine;

public class Hero : MonoBehaviour, IHero
{
    [ShowInInspector, ReadOnly] public int MaxHitPoints { get; set; }
    [ShowInInspector, ReadOnly] public int Mana { get; set; }
    [ShowInInspector, ReadOnly] public int Attack { get; set; }

    private void Awake()
    {
        MaxHitPoints = 10;
        Mana = 10;
        Attack = 10;
    }

}