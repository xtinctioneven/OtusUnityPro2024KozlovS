using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client
{
    public class ProjectileInstaller : EntityInstaller
    {
        [SerializeField] private float _moveSpeed = 3.0f;
        [SerializeField] private float _rotationSpeed = 100.0f;
        [SerializeField] private int _damage = 3;
        [SerializeField] private Team _teamId;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new ProjectileTag());
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new Rotation {Value = transform.rotation});
            entity.AddData(new RotationSpeed{Value = _rotationSpeed});
            entity.AddData(new MoveDirection {Value = transform.forward});
            entity.AddData(new MoveSpeed {Value = _moveSpeed});
            entity.AddData(new Damage {Value = _damage});
            entity.AddData(new TransformView {Value = transform});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}