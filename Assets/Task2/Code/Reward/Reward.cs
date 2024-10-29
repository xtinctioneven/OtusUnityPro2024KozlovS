using System;
using UnityEngine;

[Serializable]
public class Reward
{
    public RewardType RewardType;
    public int Amount;
    public Sprite Icon;

    public void GetReward()
    {
        Debug.Log($"Congratulations! You got {Amount} of {RewardType}!");
    }
}