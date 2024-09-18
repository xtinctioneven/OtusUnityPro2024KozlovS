using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Client
{
    [RequireComponent(typeof(Entity))]
    public class ProjectileCollisionComponent :MonoBehaviour
    {
        private EcsStartup _ecsStartup;
        private Entity _entity;

        [Inject]
        private void Construct(EcsStartup ecsStartup)
        {
            _ecsStartup = ecsStartup;
        }
        
        private void Awake()
        {
            _entity = GetComponent<Entity>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Entity target))
            {
                if (_entity.GetData<TeamId>().Value == target.GetData<TeamId>().Value)
                {
                    return;
                }
                _ecsStartup.CreateEntity(EcsWorlds.EVENTS)
                    .Add(new CollisionEnterRequest())
                    .Add(new ProjectileTag())
                    .Add(new SourceEntity { Value = _entity.Id })
                    .Add(new DamageTargetEntity { Value = target.Id });
            }
        }
    }
}