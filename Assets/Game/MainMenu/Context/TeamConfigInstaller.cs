using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "TeamConfigInstaller", menuName = "Installers/TeamConfigInstaller")]
public class TeamConfigInstaller : ScriptableObjectInstaller<TeamConfigInstaller>
{
    [SerializeField, InlineEditor] private TeamConfig _teamConfig;
    [SerializeField] private string _teamConfigId;
    public override void InstallBindings()
    {
        Container.BindInstance(_teamConfig).WithId(_teamConfigId).AsCached();
    }
}