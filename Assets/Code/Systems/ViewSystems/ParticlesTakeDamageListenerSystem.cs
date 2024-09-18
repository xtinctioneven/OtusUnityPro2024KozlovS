using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class ParticlesTakeDamageListenerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TakeDamageEvent, DamageTargetEntity>> _filter = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<UnitParticlesView> _particlesPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                int target = _filter.Pools.Inc2.Get(@event).Value;
                
                if (_particlesPool.Value.Has(target))
                {
                    ParticleSystem particleSystem = _particlesPool.Value.Get(target).TakeDamageParticles;
                    particleSystem.gameObject.SetActive(true);
                    particleSystem.Play();
                }
            }
        }
    }
}