using System;
using Game.Gameplay;
using UnityEngine;

public class DebugInput : MonoBehaviour
{
    public Action OnDebugInput;
    public static DebugInput Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(this);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDebugInput?.Invoke();
        }
    }
}

