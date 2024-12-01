using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class DefenceComponent
    {
        public int Value => _defenceStat.Value;
        private Stat _defenceStat;

        public DefenceComponent(Stat defenceStat)
        {
            if (defenceStat.StatType != StatType.Defence)
            {
                Debug.LogError("Error! Passed stat is not of type Defence!");
                return;
            }

            _defenceStat = defenceStat;
        }
    }
}