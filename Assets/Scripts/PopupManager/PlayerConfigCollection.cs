using System.Collections.Generic;
using Lessons.Architecture.PM;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [CreateAssetMenu(fileName = "PlayerConfigCollection", menuName = "Data/PlayerConfigCollection")]
    public class PlayerConfigCollection : ScriptableObject
    {
        public List<PlayerConfig> _playerConfigs = new List<PlayerConfig>();
    }
}
