using TMPro;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class CharacterStatInstaller : MonoInstaller
    {
        public const string STAT_NAME_ID = "StatName";
        public const string STAT_VALUE_ID = "StatValue";
        [SerializeField] private TMP_Text _statName;
        [SerializeField] private TMP_Text _statValue;
        public override void InstallBindings()
        {
            Container.Bind<TMP_Text>().WithId(STAT_NAME_ID).FromInstance(_statName).AsCached();
            Container.Bind<TMP_Text>().WithId(STAT_VALUE_ID).FromInstance(_statValue).AsCached();
        }
    }
}