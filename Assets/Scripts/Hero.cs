using UnityEngine;

public class Hero : MonoBehaviour
{
    public int MaxHitPoints;
    public int Mana;
    public int Attack;

    private void Awake()
    {
        MaxHitPoints = 10;
    }
}