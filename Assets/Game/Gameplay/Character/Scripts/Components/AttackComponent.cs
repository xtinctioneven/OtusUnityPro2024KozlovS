using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class AttackComponent
    {
        public int Value => _attackStat.Value;
        private Stat _attackStat;

        public AttackComponent(Stat attackStat)
        {
            if (attackStat.StatType != StatType.Attack)
            {
                Debug.LogError("Error! Passed stat is not of type Attack!");
                return;
            }

            _attackStat = attackStat;
        }
    }
}