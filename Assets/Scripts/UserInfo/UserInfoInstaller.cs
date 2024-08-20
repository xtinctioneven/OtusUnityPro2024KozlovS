using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class UserInfoInstaller : MonoInstaller
    {
        public const string NAME_TEXT_ID = "NameText";
        public const string DESCRIPTION_TEXT_ID = "DescriptionText";
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Image _avatar;
        public override void InstallBindings()
        {
            Container.Bind<TMP_Text>().WithId(NAME_TEXT_ID).FromInstance(_nameText).AsCached();
            Container.Bind<TMP_Text>().WithId(DESCRIPTION_TEXT_ID).FromInstance(_descriptionText).AsCached();
            Container.Bind<Image>().FromInstance(_avatar).AsSingle();
        }
    }
}