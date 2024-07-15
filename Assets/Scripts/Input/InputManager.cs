using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class InputManager : IGameUpdateListener, IInitializable
    {
        public event Action OnShootButtonPressed;
        public event Action<float> OnHorizontalMoveButtonPressed;

        public void Initialize()
        {
            IGameListener.Register(this);
        }

        public void OnGameUpdate(float deltaTime)
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