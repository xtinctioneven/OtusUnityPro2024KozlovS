using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public class Pool
    {
        private Transform _container;
        private GameObject _itemPrefab;
        private readonly Queue<GameObject> _poolItems;
        private bool _isFlexible = false;

        public Pool(Transform container, GameObject itemPrefab, int initialSize, bool isFlexible = false)
        {
            _container = container;
            _itemPrefab = itemPrefab;
            _poolItems = new Queue<GameObject>(initialSize);
            for (int i = 0; i < initialSize; i++)
            {
                GameObject item = GameObject.Instantiate(_itemPrefab, _container);
                item.name = itemPrefab.name + " " + i;
                _poolItems.Enqueue(item);
            }
            _isFlexible = isFlexible; 
        }

        public GameObject Spawn(Transform parentTransform, Vector3 spawnPosition)
        {
            if (!_poolItems.TryDequeue(out GameObject item))
            {
                if (_isFlexible)
                {
                    item = GameObject.Instantiate(_itemPrefab);
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
}