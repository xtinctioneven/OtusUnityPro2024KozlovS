using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        Vector3 direction = Camera.main.transform.forward;
        transform.LookAt(transform.position + direction);
    }
}