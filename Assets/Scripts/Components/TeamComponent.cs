using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public sealed class TeamComponent : UnitComponent
    {
        public bool IsPlayer { get { return _isPlayer; } }
        
        private bool _isPlayer;

        [Inject]
        private void Construct(bool isPlayer)
        {
            _isPlayer = isPlayer;
        }
    }
}