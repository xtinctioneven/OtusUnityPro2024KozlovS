using System;
using Sirenix.OdinInspector;
using UniRx;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class CharacterStat
    {

        [ShowInInspector, ReadOnly]
        public IReadOnlyReactiveProperty<string> Name => _name;
        private ReactiveProperty<string> _name = new StringReactiveProperty();

        [ShowInInspector, ReadOnly]
        public IReadOnlyReactiveProperty<int> Value => _value;
        private ReactiveProperty<int> _value = new IntReactiveProperty();
        
        [ShowInInspector, ReadOnly]
        public int LevelModifier { get; private set; }

        [Button]
        public void ChangeValue(int value)
        {
            _value.Value = value;
        }
        
        [Button]
        public void ChangeModifier(int modifier)
        {
            this.LevelModifier = modifier;
        }

        [Button]
        public void ChangeName(string name)
        {
            _name.Value = name;
        }

    }
}