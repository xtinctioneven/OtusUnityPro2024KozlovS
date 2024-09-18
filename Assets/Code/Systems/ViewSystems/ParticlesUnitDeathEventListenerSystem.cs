using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class ParticlesUnitDeathEventListenerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitParticlesView, DeathEvent>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                ParticleSystem particles = _filter.Pools.Inc1.Get(entity).DeathParticles;
                particles.gameObject.SetActive(true);
                particles.Play();
            }
        }
    }
}