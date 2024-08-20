using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class UserInfo
    {
        [ShowInInspector, ReadOnly]
        public IReadOnlyReactiveProperty<string> Name => _name;
        private ReactiveProperty<string> _name = new StringReactiveProperty();

        [ShowInInspector, ReadOnly]
        public IReadOnlyReactiveProperty<string> Description => _description;
        private ReactiveProperty<string> _description = new StringReactiveProperty();

        [ShowInInspector, ReadOnly]
        public IReadOnlyReactiveProperty<Sprite> Icon => _icon;
        private ReactiveProperty<Sprite> _icon = new ReactiveProperty<Sprite>(); 

        [Button]
        public void ChangeName(string name)
        {
            _name.Value = name;
        }

        [Button]
        public void ChangeDescription(string description)
        {
            _description.Value = description;
        }

        [Button]
        public void ChangeIcon(Sprite icon)
        {
            _icon.Value = icon;
        }
    }
}