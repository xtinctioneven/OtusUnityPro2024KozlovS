using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ChestConfigCollectionInstaller", menuName = "Installers/ChestConfigCollectionInstaller")]
public class Task2_ChestConfigCollectionInstaller : ScriptableObjectInstaller<Task2_ChestConfigCollectionInstaller>
{
    [SerializeField] private ChestConfigCollection _chestConfigCollection;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ChestConfigCollection>().FromInstance(_chestConfigCollection).AsSingle();
    }
}