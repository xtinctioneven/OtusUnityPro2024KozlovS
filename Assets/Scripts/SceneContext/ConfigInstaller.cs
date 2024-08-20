using Lessons.Architecture.PM;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Lessons.Architecture.PM
{ 
    [CreateAssetMenu(fileName = "PlayerConfigInstaller", menuName = "Installers/PlayerConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private PlayerConfigCollection _playerConfigCollection;
        public override void InstallBindings()
        {
            Container.Bind<PlayerConfigCollection>().FromInstance(_playerConfigCollection);
        }
    }
}