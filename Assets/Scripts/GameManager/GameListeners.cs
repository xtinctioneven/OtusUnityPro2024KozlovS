using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public interface IGameListener
    {
        public static event Action<IGameListener> OnRegister;

        public static void Register(IGameListener gameListener)
        {
            OnRegister?.Invoke(gameListener);
        }
    }

    public interface IGamePauseListener: IGameListener
    {
        public void OnGamePause();
    }

    public interface IGamePlayListener: IGameListener
    {
        public void OnGamePlay();
    }

    public interface IGameStartListener: IGameListener
    {
        public void OnGameStart();
    }

    public interface IGameFinishListener: IGameListener
    {
        public void OnGameFinish();
    }

    public interface IGameUpdateListener: IGameListener
    {
        public void OnGameUpdate(float deltaTime);
    }

    public interface IGameLateUpdateListener: IGameListener
    {
        public void OnGameLateUpdate(float deltaTime);
    }

    public interface IGameFixedUpdateListener: IGameListener
    {
        public void OnGameFixedUpdate(float deltaTime);
    }
}