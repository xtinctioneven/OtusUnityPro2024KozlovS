using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Helpers;
using UnityEngine;
using Zenject;

namespace Client {
    public sealed class EcsStartup : ITickable, IDisposable, IInitializable 
    {
        private EcsWorld _world;        
        private EcsWorld _events;        
        private IEcsSystems _systems;
        private EntityManager _entityManager;
        private DiContainer _diContainer;
        
        public EcsStartup(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public EcsEntityBuilder CreateEntity(string worldName = null)
        {
            return new EcsEntityBuilder(_systems.GetWorld(worldName));
        }

        public EcsWorld GetWorld(string worldName = null)
        {
            return worldName switch
            {
                null => _world,
                EcsWorlds.EVENTS => _events,
                _ => throw new Exception($"World with name {worldName} is not found!")
            };
        }
        
        public void Initialize()
        {
            _entityManager = new EntityManager();
            _world = new EcsWorld ();
            _events = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _systems.AddWorld(_events, EcsWorlds.EVENTS);
            _systems
                //Logic
                //.Add(new ExampleSystem())
                .Add(new TargetRequestSystem())
                .Add(new RotationSystem())
                .Add(new MovementSystem())
                .Add(new MoveDirectionSystem())
                .Add(new AttackOnTimerSystem())
                .Add(new ProjectileAimSystem())
                .Add(new ProjectileAttackRequestSystem())
                .Add(new MeleeAttackRequestSystem())
                .Add(new SpawnRequestSystem(_diContainer))
                .Add(new ProjectileCollisionRequestSystem())
                .Add(new TakeDamageRequestSystem())
                .Add(new HealthEmptySystem())
                .Add(new DeathRequestSystem())
                .Add(new ProjectileDeathEventSystem())
                .Add(new ClearTargetOnDeathEventSystem())
                .Add(new BaseDeathEventSystem())
                .Add(new BaseDestroyDelaySystem())

                //View
                .Add(new TransformViewSynchronizerSystem())
                .Add(new ParticlesTakeDamageListenerSystem())
                .Add(new ParticlesUnitDeathEventListenerSystem())
                .Add(new ParticlesBaseHealthThresholdListenerSystem())
                .Add(new ParticlesBaseDestroyListenerSystem())
                .Add(new AnimatorTakeDamageListenerSystem())
                .Add(new AnimatorAttackListenerSystem())
                .Add(new AnimatorDeathListenerSystem())
                .Add(new AnimatorMoveListenerSystem())
                //Editor
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.EVENTS))
#endif
                //Clean Up
                .Add(new DestroyRequestSystem())
                .Add(new GameOverEventSystem())
                .Add(new OneFrameEventSystem())
                .DelHere<DeathEvent>();
            
            _entityManager.Initialize(_world);
            _systems.Inject(_entityManager);
            _systems.Init ();
        }

        public void Tick () 
        {
            _systems?.Run ();
        }

        public void Dispose () 
        {
            if (_systems != null) 
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) 
            {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}