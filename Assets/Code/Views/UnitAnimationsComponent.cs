using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client
{
    public class UnitAnimationsComponent : MonoBehaviour
    {
        [SerializeField] private Entity _entity;

        public void RequestAttack()
        {
            if (_entity.TryGetData(out AttackRequest request) == false)
            {
                _entity.AddData(new AttackRequest());
            }
        }

        public void RequestDestroy()
        {
            if (_entity.TryGetData<DestroyRequest>(out DestroyRequest request) == false)
            {
                _entity.AddData(new DestroyRequest());
            }
        }
    }
}