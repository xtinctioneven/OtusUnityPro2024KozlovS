using System;
using Sirenix.OdinInspector;
using UniRx;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class PlayerLevel
    {
        [ShowInInspector, ReadOnly]
        public IReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
        private ReactiveProperty<int> _currentLevel = new IntReactiveProperty(1);
        
        [ShowInInspector, ReadOnly]
        public IReadOnlyReactiveProperty<int> CurrentExperience => _currentExperience;
        private ReactiveProperty<int> _currentExperience = new IntReactiveProperty();

        [ShowInInspector, ReadOnly]
        public int RequiredExperience
        {
            get { return 100 * (_currentLevel.Value + 1); }
        }

        [Button]
        public void AddExperience(int range)
        {
            var xp = Math.Min(_currentExperience.Value + range, RequiredExperience);
            _currentExperience.Value = xp;
        }

        [Button]
        public void LevelUp()
        {
            if (this.CanLevelUp())
            {
                _currentExperience.Value = 0;
                _currentLevel.Value++;
            }
        }

        public bool CanLevelUp()
        {
            return _currentExperience.Value == RequiredExperience;
        }
    }
}