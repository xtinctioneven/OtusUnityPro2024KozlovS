using System;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client
{
    [Serializable]
    public struct ProjectileWeapon
    {
        public Transform FirePoint;
        public Entity ProjectilePrefab;
    }
}