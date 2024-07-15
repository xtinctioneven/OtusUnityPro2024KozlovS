using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
   public class Pool
    {
        private Transform _container;
        private GameObject _itemPrefab;
        private Queue<GameObject> _poolItems;
        private bool _isFlexible = false;
        [Inject] DiContainer _diContainer;

        [Inject]
        private void Construct(PoolSettings poolSettings)
        {
            _container = poolSettings.Container;
            _itemPrefab = poolSettings.Prefab;
            _isFlexible = poolSettings.IsFlexible;
            _poolItems = new Queue<GameObject>(poolSettings.PoolSize);
            for (int i = 0; i < poolSettings.PoolSize; i++)
            {
                GameObject item = _diContainer.InstantiatePrefab(_itemPrefab, _container);
                item.name = _itemPrefab.name + " " + i;
                _poolItems.Enqueue(item);
            }
        }
        
        public GameObject Spawn(Transform parentTransform, Vector3 spawnPosition)
        {
            if (!_poolItems.TryDequeue(out GameObject item))
            {
                if (_isFlexible)
                {
                    item = _diContainer.InstantiatePrefab(_itemPrefab);
                }
                else
                {
                    return null;
                }
            }

            item.transform.SetParent(parentTransform);
            item.transform.position = spawnPosition;
            return item;
        }

        public void Unspawn(GameObject item)
        {
            item.transform.SetParent(_container);
            _poolItems.Enqueue(item);
        }

        public int GetSize()
        {
            return _poolItems.Count;
        }
    }

    [Serializable]
    public struct PoolSettings
    {
        public Transform Container;
        public GameObject Prefab;
        public int PoolSize;
        public bool IsFlexible;
    }
}