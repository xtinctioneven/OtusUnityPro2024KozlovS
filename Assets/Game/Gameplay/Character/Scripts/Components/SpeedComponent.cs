using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class SpeedComponent
    {
        public int Value => _initiativeStat.Value;
        private Stat _initiativeStat;

        public SpeedComponent(Stat initiativeStat)
        {
            if (initiativeStat.StatType != StatType.Initiative)
            {
                Debug.LogError("Error! Passed stat is not of type Initiative!");
                return;
            }

            _initiativeStat = initiativeStat;
        }
    }
}