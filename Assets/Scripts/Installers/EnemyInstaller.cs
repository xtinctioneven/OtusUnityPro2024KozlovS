using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyInstaller : UnitInstaller
    {
        [SerializeField] private float _attackTime = 1f;
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.BindInterfacesAndSelfTo<EnemyMoveAgent>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAttackAgent>().FromNew().AsSingle().WithArguments(_attackTime);
        }
    }
}
