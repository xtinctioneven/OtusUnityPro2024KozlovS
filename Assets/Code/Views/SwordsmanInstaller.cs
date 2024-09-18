using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client
{
    public class SwordsmanInstaller : UnitInstaller
    {
        private Entity _thisEntity;
        [SerializeField] private int _attackDamage;
        
        protected override void Install(Entity entity)
        {
            base.Install(entity);
            entity.AddData(new Damage {Value =  _attackDamage});
        }
    }
}