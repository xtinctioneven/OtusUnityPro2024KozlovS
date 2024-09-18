using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [Serializable]
    public struct UnitParticlesView
    {
        public ParticleSystem TakeDamageParticles;
        public ParticleSystem DeathParticles;
    }
}