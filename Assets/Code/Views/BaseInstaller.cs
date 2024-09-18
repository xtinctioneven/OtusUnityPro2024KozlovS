using System;
using System.Collections.Generic;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client
{
    public class BaseInstaller : EntityInstaller
    {
        [SerializeField] private int _health ;
        [SerializeField] private Team _teamId;
        [SerializeField] private List<ParticleSystemEntry> _particleSystems = new List<ParticleSystemEntry>();

        protected override void Install(Entity entity)
        {
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new TransformView {Value = transform});
            entity.AddData(new Health {Value = _health});
            entity.AddData(new DamageableTag());
            entity.AddData(new TeamId {Value = _teamId});
            entity.AddData(new BaseTag());
            entity.AddData(new BaseParticlesView {ParticleSystemEntries = _particleSystems});
        }

        protected override void Dispose(Entity entity)
        {
            
        }

        public Team GetTeam()
        {
            return _teamId;
        }public class BaseParticlesComponent : MonoBehaviour
        {
            
            [SerializeField] private Entity _entity;
        }
    }
    
    [Serializable]
    public struct ParticleSystemEntry
    {
        public string Name;
        public int HealthActivateThreshold;
        public ParticleSystem ParticleSystem;
    }
}
