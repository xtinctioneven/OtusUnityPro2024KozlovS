using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;

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
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}