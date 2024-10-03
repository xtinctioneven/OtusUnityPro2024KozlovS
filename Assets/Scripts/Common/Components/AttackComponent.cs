using System;

[Serializable]
public class AttackComponent
{
    public int Value; 

    public AttackComponent(int attackValue)
    {
        Value = attackValue;
    }
}