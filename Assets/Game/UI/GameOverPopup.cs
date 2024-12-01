using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class GameOverPopup : MonoBehaviour
    {
        [SerializeField]
        private Image backgroundImage;
        [SerializeField]
        private TMP_Text gameOverText;

        [Sirenix.OdinInspector.Button]
        public void SetActive(bool isActive)
        {
            this.gameObject.SetActive(isActive);
        }

        public void SetText(string text)
        {
            gameOverText.text = text;
        }
    }
}