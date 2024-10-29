using UnityEngine;

[CreateAssetMenu(fileName = "ChestConfigCollection", menuName = "Configs/New ChestConfigCollection")]
public class ChestConfigCollection : ScriptableObject
{
    public ChestConfig[] Configs;
}