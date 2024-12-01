using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public sealed class UIService : MonoBehaviour
    {
        [SerializeField]
        private HeroListView bluePlayer;

        [SerializeField]
        private HeroListView redPlayer;
        
        [SerializeField]
        private GameOverPopup gameOverPopup;

        public HeroListView GetBluePlayer()
        {
            return this.bluePlayer;
        }

        public HeroListView GetRedPlayer()
        {
            return this.redPlayer;
        }

        public GameOverPopup GetGameOverPopup()
        {
            return this.gameOverPopup;
        }
    }
}