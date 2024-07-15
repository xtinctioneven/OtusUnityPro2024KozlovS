using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameManager: IInitializable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {

        private enum State
        {
            Idle = 0,
            Start = 5,
            Play = 10,
            Pause = 15,
            Finish = 20
        }

        private List<IGameListener> _listeners = new();
        private List<IGameUpdateListener> _updateListeners = new();
        private List<IGameLateUpdateListener> _lateUpdateListeners = new();
        private List<IGameFixedUpdateListener> _fixedUpdateListeners = new();

        [SerializeField] State _state = State.Idle;

        public void Initialize()
        {
            IGameListener.OnRegister += IGameListener_OnRegister;
            IGameListener.OnDeregister += IGameListener_OnDeregister;
        }

        public void Dispose()
        {
            IGameListener.OnRegister -= IGameListener_OnRegister;
            IGameListener.OnDeregister -= IGameListener_OnDeregister;
        }

        public void Tick()
        {
            
            if (_state != State.Play && _state != State.Start)
            {
                return;
            }

            float deltaTime = Time.deltaTime;

            if (_state == State.Start)
            {
                for (int i = 0; i < _updateListeners.Count; i++)
                {
                    if (_updateListeners[i] is IGameStartListener)
                    {
                        _updateListeners[i].OnGameUpdate(deltaTime);
                    }
                }
            }
            else
            {
                for (int i = 0; i < _updateListeners.Count; i++)
                {
                    _updateListeners[i].OnGameUpdate(deltaTime);
                }
            }

        }

        public void FixedTick()
        {
            if (_state != State.Play)
            {
                return;
            }

            float deltaTime = Time.fixedDeltaTime;
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnGameFixedUpdate(deltaTime);
            }
        }

        public void LateTick()
        {
            if (_state != State.Play)
            {
                return;
            }

            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _lateUpdateListeners.Count; i++)
            {
                _lateUpdateListeners[i].OnGameLateUpdate(deltaTime);
            }
        }

        private void IGameListener_OnRegister(IGameListener registeredListener)
        {
            AddListener(registeredListener);
        }

        public void AddListener(IGameListener newListener)
        {
            _listeners.Add(newListener);

            if (newListener is IGameUpdateListener updateListener)
            {
                _updateListeners.Add(updateListener);
            }

            if (newListener is IGameLateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Add(lateUpdateListener);
            }

            if (newListener is IGameFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Add(fixedUpdateListener);
            }
        }

        private void IGameListener_OnDeregister(IGameListener deregisteredListener)
        {
            RemoveListener(deregisteredListener);
        }

        public void RemoveListener(IGameListener removedListener)
        {
            _listeners.Remove(removedListener);

            if (removedListener is IGameUpdateListener updateListener)
            {
                _updateListeners.Remove(updateListener);
            }

            if (removedListener is IGameLateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Remove(lateUpdateListener);
            }

            if (removedListener is IGameFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Remove(fixedUpdateListener);
            }
        }

        public void StartGame()
        {
            _state = State.Start;
            foreach (IGameListener listener in _listeners)
            {
                if (listener is IGameStartListener)
                {
                    (listener as IGameStartListener).OnGameStart();
                }
            }
        }

        public void PlayGame()
        {
            _state = State.Play;
            foreach (IGameListener listener in _listeners)
            {
                if (listener is IGamePlayListener)
                {
                    (listener as IGamePlayListener).OnGamePlay();
                }
            }
        }

        public void PauseGame()
        {
            _state = State.Pause;
            foreach (IGameListener listener in _listeners)
            {
                if (listener is IGamePauseListener)
                {
                    (listener as IGamePauseListener).OnGamePause();
                }
            }
        }

        public void TogglePause()
        {
            if (_state == State.Pause)
            {
                PlayGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void FinishGame()
        {
            _state = State.Finish;
            Debug.Log("Game Over!");
            foreach (IGameListener listener in _listeners)
            {
                if (listener is IGameFinishListener)
                {
                    (listener as IGameFinishListener).OnGameFinish();
                }
            }
        }
    }
}