using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyPositions
    {
        private Transform[] _spawnPositions;
        private Transform[] _attackPositions;

        [Inject]
        public void Construct(EnemyPositionsSettings settings)
        {
            _spawnPositions = settings.SpawnPositions;
            _attackPositions = settings.AttackPositions;
        }

        public Vector3 GetSpawnPosition()
        {
            return RandomTransform(_spawnPositions).position;
        }

        public Vector3 GetAttackPosition()
        {
            return RandomTransform(_attackPositions).position;
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = UnityEngine.Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }

    [Serializable]
    public struct EnemyPositionsSettings
    {
        public Transform[] SpawnPositions;
        public Transform[] AttackPositions;
    }
}