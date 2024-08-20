using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PlayerLevelInstaller : MonoInstaller
    {
        public const string LEVEL_TEXT_ID = "LevelText";
        public const string XP_TEXT_ID = "XPText";
        public const string LEVEL_BUTTON_ID = "LevelButton";
        public const string XP_BUTTON_ID = "XPButton";
        [SerializeField] private Image _fillImage;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _XPText;
        [SerializeField] private Button _levelUpButton;
        [SerializeField] private Button _addXPButton;
        public override void InstallBindings()
        {
            Container.Bind<Image>().FromInstance(_fillImage).AsSingle();
            Container.Bind<TMP_Text>().WithId(LEVEL_TEXT_ID).FromInstance(_levelText).AsCached();
            Container.Bind<TMP_Text>().WithId(XP_TEXT_ID).FromInstance(_XPText).AsCached();
            Container.Bind<Button>().WithId(LEVEL_BUTTON_ID).FromInstance(_levelUpButton).AsCached();
            Container.Bind<Button>().WithId(XP_BUTTON_ID).FromInstance(_addXPButton).AsCached();
        }
    }
}
