using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "ConfigInstaller",
        menuName = "Config/New ConfigInstaller"
        )]
    public class ConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private BulletConfig _bulletConfig;

        public override void InstallBindings()
        {
            Container.Bind<BulletConfig>().FromInstance(_bulletConfig).AsSingle();
        }
    }
}
