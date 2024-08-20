using System;
using System.Linq;
using Sirenix.OdinInspector;
using UniRx;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class CharacterInfo
    {
        [ShowInInspector]
        public IReadOnlyReactiveCollection<CharacterStat> CharacterStats => _characterStats;
        private ReactiveCollection<CharacterStat> _characterStats = new();

        [Button]
        public void AddStat(CharacterStat stat)
        {
            if (!_characterStats.Contains(stat))
            {
                _characterStats.Add(stat);
            }
        }

        [Button]
        public void RemoveStat(CharacterStat stat)
        {
            _characterStats.Remove(stat);
        }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in _characterStats)
            {
                if (stat.Name.Value == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStat[] GetStats()
        {
            return _characterStats.ToArray();
        }
    }
}