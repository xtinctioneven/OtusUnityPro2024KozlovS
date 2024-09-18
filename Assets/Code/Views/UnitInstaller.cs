using System;
using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client
{
    public abstract class UnitInstaller : EntityInstaller
    {
        [SerializeField] private float _moveSpeed = 5.0f;
        [SerializeField] private float _rotationSpeed = 10.0f;
        [SerializeField] private int _health ;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _attackTime = 2.0f;
        [SerializeField] private float _attackDistance = 4.0f;
        [SerializeField] private Team _teamId;
        [SerializeField] private Renderer[] _colorfulParts;
        [SerializeField] private ParticleSystem _takeDamageParticles;
        [SerializeField] private ParticleSystem _deathParticles;

        protected override void Install(Entity entity)
        {
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new Rotation {Value = transform.rotation});
            entity.AddData(new RotationSpeed {Value = _rotationSpeed});
            entity.AddData(new MoveDirection {Value = Vector3.zero});
            entity.AddData(new MoveSpeed {Value = _moveSpeed});
            entity.AddData(new TransformView {Value = transform});
            entity.AddData(new AnimatorView {Value = _animator});
            entity.AddData(new Health {Value = _health});
            entity.AddData(new DamageableTag());
            entity.AddData(new TeamId {Value = _teamId});
            entity.AddData(new TargetRequest());
            entity.AddData(new Timer {CurrentValue = 0f, TimerValue = _attackTime});
            entity.AddData(new AttackDistance {Value = _attackDistance});
            entity.AddData(new UnitParticlesView
            {
                DeathParticles = _deathParticles,
                TakeDamageParticles = _takeDamageParticles
            });
        }

        protected override void Dispose(Entity entity)
        {
            
        }

        public void SetTeam(Team teamId)
        {
            _teamId = teamId;
        }

        public void Recolor(Material newMaterial)
        {
            for (int i = 0; i < _colorfulParts.Length; i++)
            {
                _colorfulParts[i].material = newMaterial;
            }
        }
    }
}