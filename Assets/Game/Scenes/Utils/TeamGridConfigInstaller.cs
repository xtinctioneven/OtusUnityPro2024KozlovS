using Game.Gameplay;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "TeamGridConfig", menuName = "Installers/TeamGridConfig")]
public class TeamGridConfigInstaller : ScriptableObjectInstaller<TeamGridConfigInstaller>
{
    public CharacterFactory CharacterFactory;
    public CharacterConfig[] Characters;
    public Vector2[] Positions;
    public TeamGridData[] TeamGridData { get; private set; }
    public string InjectId;
    
    public override void InstallBindings()
    {
        int length = Characters.Length <= Positions.Length ? Characters.Length : Positions.Length;
        TeamGridData = new TeamGridData[length];
        for (int i = 0; i < length; i++)
        {
            TeamGridData[i] = new TeamGridData()
            {
                CharacterConfig = Characters[i],
                Position = Positions[i]
            };
        }
        Container.Bind<TeamGridData[]>().WithId(InjectId).FromInstance(TeamGridData).AsCached().NonLazy();
    }
}