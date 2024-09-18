using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class ParticlesBaseHealthThresholdListenerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BaseParticlesView, Health>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<BaseParticlesView> particleViewPool = _filter.Pools.Inc1;
            EcsPool<Health> healthPool = _filter.Pools.Inc2;
            foreach (int entity in _filter.Value)
            {
                List<ParticleSystemEntry> particleSystemEntry = particleViewPool.Get(entity).ParticleSystemEntries;
                int health = healthPool.Get(entity).Value;
                foreach (var entry in particleSystemEntry)
                {
                    if (entry.HealthActivateThreshold >= health)
                    {
                        entry.ParticleSystem.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}