using UnityEngine;
using Zenject;

namespace SampleGame
{
    public class GameSceneUIInstaller : MonoInstaller
    {
        [SerializeField] private PauseButton _pauseButton;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PauseButton>().FromInstance(_pauseButton).AsSingle();
        }
    }
}