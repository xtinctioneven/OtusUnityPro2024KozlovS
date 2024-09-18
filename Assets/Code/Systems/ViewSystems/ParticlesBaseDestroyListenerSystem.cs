using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    public class ParticlesBaseDestroyListenerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BaseParticlesView, DeathEvent>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<BaseParticlesView> particleViewPool = _filter.Pools.Inc1;
            foreach (int entity in _filter.Value)
            {
                List<ParticleSystemEntry> particleSystemEntry = particleViewPool.Get(entity).ParticleSystemEntries;
                foreach (var entry in particleSystemEntry)
                {
                    if (entry.Name == "Destroy")
                    {
                        entry.ParticleSystem.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}