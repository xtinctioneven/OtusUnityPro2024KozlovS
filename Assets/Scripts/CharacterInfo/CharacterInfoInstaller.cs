using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class CharacterInfoInstaller : MonoInstaller
    {
        [SerializeField] private CharacterStatView _characterStatViewPrefab;
        [SerializeField] private Transform _statsViewContainer;
        public override void InstallBindings()
        {
            Container.Bind<CharacterStatView>().FromInstance(_characterStatViewPrefab).AsSingle();
            Container.Bind<Transform>().FromInstance(_statsViewContainer).AsSingle();
        }
    }
}
