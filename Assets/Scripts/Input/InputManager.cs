using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public event Action OnShootButtonPressed;
        public event Action<float> OnHorizontalMoveButtonPressed;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShootButtonPressed?.Invoke();
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                float HorizontalDirection = -1;
                OnHorizontalMoveButtonPressed?.Invoke(HorizontalDirection);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                float HorizontalDirection = 1;
                OnHorizontalMoveButtonPressed?.Invoke(HorizontalDirection);
            }
        }
    }
}