using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public class StartupInstaller : MonoInstaller
    {
        [SerializeField] private Image _backgroundFade;
        [SerializeField] private TextMeshProUGUI _countdownText;
        [SerializeField] private Button _startButton;
        [SerializeField] private GameStartupSettings _startupSettings;
        public override void InstallBindings()
        {
            Container.Bind<Button>().FromInstance(_startButton);
            Container.Bind<Image>().FromInstance(_backgroundFade);
            Container.Bind<TextMeshProUGUI>().FromInstance(_countdownText);
            Container.BindInterfacesAndSelfTo<GameStartup>().FromNew().AsSingle().WithArguments(_startupSettings);
        }
    }
}