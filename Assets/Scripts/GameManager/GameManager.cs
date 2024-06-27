using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
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

        private void Awake()
        {
            IGameListener.OnRegister += IGameListener_OnRegister;
        }

        private void OnDestroy()
        {
            IGameListener.OnRegister -= IGameListener_OnRegister;
        }

        private void Update()
        {
            if (_state != State.Play)
            {
                return;
            }

            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnGameUpdate(deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (_state != State.Play || _state != State.Start)
            {
                return;
            }

            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _lateUpdateListeners.Count; i++)
            {
                _lateUpdateListeners[i].OnGameLateUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
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

        private void IGameListener_OnRegister(IGameListener registeredListener)
        {
            AddListener(registeredListener);
        }

        public void AddListener(IGameListener newListener)
        {
            _listeners.Add(newListener);

            if (newListener is IGameUpdateListener)
            {
                _updateListeners.Add(newListener as IGameUpdateListener);
            }

            if (newListener is IGameLateUpdateListener)
            {
                _lateUpdateListeners.Add(newListener as IGameLateUpdateListener);
            }

            if (newListener is IGameFixedUpdateListener)
            {
                _fixedUpdateListeners.Add(newListener as IGameFixedUpdateListener);
            }
        }

        public void RemoveListener(IGameListener removedListener)
        {
            _listeners.Remove(removedListener);

            if (removedListener is IGameUpdateListener)
            {
                _updateListeners.Remove(removedListener as IGameUpdateListener);
            }

            if (removedListener is IGameLateUpdateListener)
            {
                _lateUpdateListeners.Remove(removedListener as IGameLateUpdateListener);
            }

            if (removedListener is IGameFixedUpdateListener)
            {
                _fixedUpdateListeners.Remove(removedListener as IGameFixedUpdateListener);
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