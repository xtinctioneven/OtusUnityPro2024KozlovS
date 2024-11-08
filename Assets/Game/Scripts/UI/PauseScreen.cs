using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SampleGame
{
    public sealed class PauseScreen : MonoBehaviour
    {
        [SerializeField]
        private Button resumeButton;

        [SerializeField]
        private Button exitButton;

        private MenuLoader menuLoader;
        private GameLoader gameLoader;
        private PauseButton pauseButton;

        [Inject]
        public void Construct(MenuLoader menuLoader, GameLoader gameLoader, PauseButton pauseButton)
        {
            this.menuLoader = menuLoader;
            this.gameObject.SetActive(false);
            this.gameLoader = gameLoader;
            this.pauseButton = pauseButton;
        }

        private void OnEnable()
        {
            this.pauseButton.Unsubscribe(Show);
            this.resumeButton.onClick.AddListener(this.Hide);
            this.exitButton.onClick.AddListener(this.LoadMenuScene);
        }

        private void OnDisable()
        {
            this.pauseButton.Subscribe(Show);
            this.resumeButton.onClick.RemoveListener(this.Hide);
            this.exitButton.onClick.RemoveListener(this.LoadMenuScene);
        }

        public void Show()
        {
            Time.timeScale = 0; //KISS
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            Time.timeScale = 1; //KISS
            this.gameObject.SetActive(false);
        }

        private async void LoadMenuScene()
        {
            Time.timeScale = 1;
            await this.menuLoader.LoadMenu();
            await this.gameLoader.UnloadGame();
        }
    }
}