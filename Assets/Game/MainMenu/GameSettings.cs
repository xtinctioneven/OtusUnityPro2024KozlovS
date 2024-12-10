using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField, InlineEditor] private TeamConfig _availableHeroes;
}
