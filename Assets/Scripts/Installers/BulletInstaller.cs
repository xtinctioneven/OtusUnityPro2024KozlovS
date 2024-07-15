using ShootEmUp;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var bullet = this.gameObject.GetComponent<Bullet>();
            Container.Bind<Rigidbody2D>().FromComponentSibling().AsSingle();
            Container.Bind<SpriteRenderer>().FromComponentSibling().AsSingle();
            Container.BindInterfacesAndSelfTo<Bullet>().FromInstance(bullet).AsSingle();
        }
    }
}