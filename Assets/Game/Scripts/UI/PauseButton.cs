using System;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class PauseButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        
        [SerializeField]
        private PauseScreen pauseScreen;

        private void OnDisable()
        {
            this.button.onClick.RemoveAllListeners();
        }

        public void Subscribe(Action callback)
        {
            this.button.onClick.AddListener(callback.Invoke);
        }

        public void Unsubscribe(Action callback)
        {
            this.button.onClick.RemoveListener(callback.Invoke);
        }
    }
}