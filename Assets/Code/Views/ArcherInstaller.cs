using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client
{
    public class ArcherInstaller : UnitInstaller
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Entity _projectilePrefab;
        private Entity _thisEntity;

        protected override void Install(Entity entity)
        {
            base.Install(entity);
            entity.AddData(new ProjectileWeapon
            {
                ProjectilePrefab = _projectilePrefab,
                FirePoint = _firePoint
            });
        }
    }
}