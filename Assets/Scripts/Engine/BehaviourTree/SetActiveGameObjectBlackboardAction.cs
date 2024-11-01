using System;
using Atomic.AI;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class SetActiveGameObjectBlackboardAction : IBlackboardAction
    {
        [SerializeField, BlackboardKey] private int _gameObjectKey;
        [SerializeField] private bool _isActive;
        public void Invoke(IBlackboard blackboard)
        {
            GameObject gameObject = blackboard.GetObject<GameObject>(_gameObjectKey);
            gameObject.SetActive(_isActive);
        }
    }
}