using ShootEmUp;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterInstaller : UnitInstaller
    {
        public const string CHARACTER_ID = "Character";
        public override void InstallBindings()
        {
            //Player + Character
            base.InstallBindings();
            Container.BindInterfacesAndSelfTo<InputManager>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterController>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterDeathObserver>().FromNew().AsSingle();
        }
    }
}