using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class UnitInstaller : MonoInstaller
    {
        [SerializeField] private bool _isPlayer;
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _speed = 5.0f;
        public override void InstallBindings()
        {
            var rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
            var unit = this.gameObject.GetComponent<Unit>();
            Container.Bind<GameObject>().FromInstance(this.gameObject).AsSingle();
            Container.Bind<Rigidbody2D>().FromInstance(rigidbody2D).AsSingle();
            Container.Bind<Transform>().FromInstance(transform.GetChild(0)).AsSingle();
            Container.Bind<WeaponComponent>().FromNew().AsSingle();
            Container.Bind<MoveComponent>().FromNew().AsSingle().WithArguments(_speed);
            Container.Bind<TeamComponent>().FromNew().AsSingle().WithArguments(_isPlayer);
            Container.Bind<HitPointsComponent>().FromNew().AsSingle().WithArguments(_maxHealth);
            Container.Bind<Unit>().FromInstance(unit).AsSingle();
        }
    }
}
