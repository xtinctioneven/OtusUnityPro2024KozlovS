using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestConfig", menuName = "Configs/New ChestConfig")]
public class ChestConfig : ScriptableObject
{
    public string Id;
    public Sprite OpenChestImage;
    public Sprite ClosedChestImage;
    public SerializedTimeSpan TimeToOpenChest;
    public Reward[] Rewards;

    [Serializable]
    public struct SerializedTimeSpan
    {
        public int Hours;
        public int Minutes;
        public int Seconds;
    }
}